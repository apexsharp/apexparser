using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiscUtil.Collections;
using MiscUtil.Collections.Extensions;

namespace ApexParser.Toolbox
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

        public static SmartEnumerable<T> AsSmart<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.EmptyIfNull().AsSmartEnumerable();
        }
    }
}
