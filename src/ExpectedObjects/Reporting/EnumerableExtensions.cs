using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpectedObjects.Reporting
{
    static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int, IEnumerable<T>> action)
        {
            var list = enumerable.ToList();

            for (int i = 0; i < list.Count; i++)
            {
                action(list[i], i, list);
            }
        }
    }
}