using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToolBox.Reflection;

namespace ToolBox.Expressions
{
    public static class QueryableExtensions
    {
        #region Dynamic Select

        class PropertyDescription
        {
            public string Name { get; set; }

            public Type Type { get; set; }

            public Expression AssignmentExpression { get; set; }
        }

        private static PropertyDescription GetPropertyDescription<TSource>(string name, Expression<Func<TSource, object>> expr, ParameterExpression parameter)
        {
            Expression cleanExpression;

            switch (expr.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expr.Body as UnaryExpression;
                    cleanExpression = ((ue != null) ? ue.Operand : null);
                    break;
                default:
                    cleanExpression = expr.Body;
                    break;
            }

            return new PropertyDescription
            {
                Name = name,
                Type = cleanExpression.Type,
                AssignmentExpression = ParameterRebinder.ReplaceParameter(expr.Parameters[0], parameter, cleanExpression)
            };
        }

        public static IQueryable SelectDynamic<TSource>(this IQueryable<TSource> source,
            Dictionary<string, Expression<Func<TSource, object>>> fields)
        {
            var cacheKey = ExpressionCache.GetCacheKey<TSource>(fields.Select(s => s.Key).OrderBy(x => x));

            var lambda = (LambdaExpression)ExpressionCache.GetOrAdd(cacheKey, () => BuildDynamicTypeSelector(fields));

            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable), "Select",
                    new Type[] { source.ElementType, lambda.Body.Type },
                    source.Expression, Expression.Quote(lambda)));
        }

        private static LambdaExpression BuildDynamicTypeSelector<TSource>(Dictionary<string, Expression<Func<TSource, object>>> fields)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "t");

            IEnumerable<PropertyDescription> propertiesDescription = fields.Select(s => GetPropertyDescription(s.Key, s.Value, parameter)).ToList();

            var dynamicTypeProperties = propertiesDescription.Select(x => new DynamicProperty(x.Name, x.Type));
            Type dynamicType = ClassFactory.Instance.GetDynamicClass(dynamicTypeProperties);

            IEnumerable<MemberBinding> bindings = dynamicType.GetProperties()
                .Select(p => Expression.Bind(p, propertiesDescription.First(x => x.Name == p.Name).AssignmentExpression));
            var memberInit = Expression.MemberInit(Expression.New(dynamicType), bindings);

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), dynamicType);

            return Expression.Lambda(delegateType, memberInit, parameter);
        }

        #endregion

        #region AutoMapping

        public static ProjectionExpression<TSource> Project<TSource>(this IQueryable<TSource> source)
        {
            return new ProjectionExpression<TSource>(source);
        }

        #endregion
    }
}
