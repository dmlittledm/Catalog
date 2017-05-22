using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ToolBox.Expressions
{
    public static class ExpressionCache
    {
        private static Dictionary<string, Expression> _expressionCache = new Dictionary<string, Expression>();

        public static Expression GetOrAdd(string cacheKey, Func<Expression> createMethod)
        {
            if (!_expressionCache.ContainsKey(cacheKey))
                _expressionCache.Add(cacheKey, createMethod());

            return _expressionCache[cacheKey];
        }

        public static string GetCacheKey<TSource>(IEnumerable<string> dynamicFields)
        {
            return typeof(TSource).FullName + ";" + string.Join(";", dynamicFields);
        }

        public static string GetCacheKey<TSource, TResult>()
        {
            return typeof(TSource).FullName + ";" + typeof(TResult).FullName;
        }
    }
}
