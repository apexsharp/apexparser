using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEnumerable = System.Collections.IEnumerable;

namespace ApexSharp.ApexParser.Toolbox
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, params T[] values)
        {
            return enumerable.Except(values.AsEnumerable());
        }

        public static SmartEnumerable<T> AsSmart<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.EmptyIfNull().AsSmartEnumerable();
        }

        public static IEnumerable<T> OfExactType<T>(this IEnumerable enumerable)
        {
            return enumerable.OfType<T>().Where(t => t.GetType() == typeof(T));
        }
    }
}
