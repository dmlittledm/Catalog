﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolBox.Utils
{
    public static class ExceptionUtils
    {
        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem,
            Func<TSource, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<TSource> FromHierarchy<TSource>(
            this TSource source,
            Func<TSource, TSource> nextItem)
            where TSource : class
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }

        public static string GetAllMessages(this Exception exception, string delimeter = null)
        {
            var messages = exception.FromHierarchy(ex => ex.InnerException)
                   .Select(ex => ex.Message);

            return String.Join(delimeter ?? " ---> ", messages);
        }
    }
}
