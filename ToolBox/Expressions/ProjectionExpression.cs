using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ToolBox.Utils;

namespace ToolBox.Expressions
{
    public class ProjectionExpression<TSource>
    {
        private readonly IQueryable<TSource> _source;

        public ProjectionExpression(IQueryable<TSource> source)
        {
            _source = source;
        }

        public IQueryable<TResult> To<TResult>()
        {
            var cacheKey = ExpressionCache.GetCacheKey<TSource, TResult>();

            var queryExpression = ExpressionCache.GetOrAdd(cacheKey, () => BuildExpression<TResult>());

            return _source.Select((Expression<Func<TSource, TResult>>)queryExpression);
        }

        private static Expression<Func<TSource, TResult>> BuildExpression<TResult>()
        {
            var srcProps = typeof(TSource).GetProperties();
            var destProps = typeof(TResult).GetProperties().Where(dest => dest.CanWrite);
            var paramExpr = Expression.Parameter(typeof(TSource), "src");

            var bindings = destProps
                      .Select(destProp => BuildBinding(paramExpr, destProp, srcProps))
                      .Where(binding => binding != null);

            return Expression.Lambda<Func<TSource, TResult>>(
                      Expression.MemberInit(
                        Expression.New(typeof(TResult)), bindings), paramExpr);
        }

        private static MemberAssignment BuildBinding(Expression paramExpr, MemberInfo destProp,
                                IEnumerable<PropertyInfo> srcProps)
        {
            var srcProp = srcProps.FirstOrDefault(src => src.Name == destProp.Name);

            if (srcProp != null)
            {
                return Expression.Bind(destProp, Expression.Property(paramExpr, srcProp));
            }

            var propNames = destProp.Name.SplitCamelCase();

            if (propNames.Length == 2)
            {
                srcProp = srcProps.FirstOrDefault(src => src.Name == propNames[0]);

                if (srcProp != null)
                {
                    var srcChildProps = srcProp.PropertyType.GetProperties();
                    var srcChildProp = srcChildProps.FirstOrDefault(src => src.Name == propNames[1]);

                    if (srcChildProp != null)
                    {
                        return Expression.Bind(destProp,
                              Expression.Property(
                                Expression.Property(paramExpr, srcProp), srcChildProp));
                    }
                }
            }

            return null;
        }
    }
}
