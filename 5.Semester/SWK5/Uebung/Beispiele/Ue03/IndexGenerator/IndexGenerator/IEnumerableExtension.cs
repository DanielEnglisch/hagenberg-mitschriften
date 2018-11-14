using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexGenerator
{
    public static class IEnumerableExtension
    {

        public static T MaxBy<T>(this IEnumerable<T> items, Comparison<T> comparison)
        {
            if (!items.Any())
            {
                throw new InvalidOperationException();
            }


            T max = items.First();

            foreach(T item in items.Skip(1))
            {
                if(comparison(item, max) > 0)
                {
                        max = item;
                }
                        
            }

                return max;
        }
    }
}
