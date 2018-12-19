using System;
using System.Collections.Generic;
using System.Linq;

namespace IndexGenerator
{
    // stattdessen comparison verwenden
    // delegate int Comparison<T>(T x, T y);

    public static class IEnumerableExtensions
    {
        // ohne this ist es keine extensionmethod
        public static T MaxBy<T>(this IEnumerable<T> items, Comparison<T> comparison)
        {
            // irgendein element drin?
            if (!items.Any())
            {
                throw new InvalidOperationException();
            }

            T max = items.First();

            foreach (T item in items.Skip(1))
            {
                if (comparison(item, max) > 0)
                    max = item;
            }

            return max;
        }
    }
}
