using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace ToolBox.Expressions
{
    public static class FilterExpressionUtils
    {
        private static readonly ConcurrentDictionary<Expression, object> _cachedFunctions
                = new ConcurrentDictionary<Expression, object>();

        public static Func<T, bool> AsFunc<T>(Expression<Func<T, bool>> expr)
            where T : class
        {
            //@see http://sergeyteplyakov.blogspot.ru/2015/06/lazy-trick-with-concurrentdictionary.html
            return (Func<T, bool>)_cachedFunctions.GetOrAdd(expr, id => new Lazy<object>(
                    () => _cachedFunctions.GetOrAdd(id, expr.Compile())));
        }

        public static bool Is<T>(this T entity, Expression<Func<T, bool>> expr)
            where T : class
        {
            Contract.Requires(expr != null);
            return AsFunc(expr).Invoke(entity);
        }

        public static CompositeFilterExpression<T> FilterRule<T>(this Expression<Func<T, bool>> expr) where T : class
        {
            return expr;
        }

        public static CompositeFilterExpression<T> AndMerge<T>(this IEnumerable<CompositeFilterExpression<T>> elements) where T : class
        {
            return elements.Merge((l, r) => l & r);
        }

        public static CompositeFilterExpression<T> OrMerge<T>(this IEnumerable<CompositeFilterExpression<T>> elements) where T : class
        {
            return elements.Merge((l, r) => l | r);
        }

        private static CompositeFilterExpression<T> Merge<T>(this IEnumerable<CompositeFilterExpression<T>> elements, 
            Func<CompositeFilterExpression<T>, CompositeFilterExpression<T>, CompositeFilterExpression<T>> mergeFunc) where T : class
        {
            if (elements == null || mergeFunc == null) return null;

            var enumerator = elements.GetEnumerator();
            if (!enumerator.MoveNext()) return null;

            var result = enumerator.Current;
            while (enumerator.MoveNext())
            {
                result = mergeFunc(result, enumerator.Current);
            }
            return result;
        }
    }
}
