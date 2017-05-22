using System.Collections.Generic;

namespace ToolBox.Utils
{
    public static class ArrayUtils
    {
        public static IEnumerable<T> GetEvenItems<T>(T[] array)
        {
            return GetItemsOfType<T>(array, true);
        }

        public static IEnumerable<T> GetUnevenItems<T>(T[] array)
        {
            return GetItemsOfType<T>(array, false);
        }

        public static IEnumerable<T> GetItemsOfType<T>(T[] array, bool even)
        {
            var index = even ? 1 : 0;

            while (index < array.Length)
            {
                yield return array[index];
                index += 2;
            }
        }
    }
}
