using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingCalculator.Tests.Steps
{
    internal static class EnumerableExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> items, Action<T> actOn)
        {
            if (actOn == null) return;
            items.ForEach((index, item) => actOn(item));
        }

        internal static void ForEach<T>(this IEnumerable<T> items, Action<int,T> actOn)
        {
            if (actOn == null) return;
            var index = 0;
            foreach (var item in items)
            {
                actOn(index, item);
                index++;
            }
        }
    }
}