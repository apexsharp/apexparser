// SmartEnumerable by Jon Skeet
// http://msmvps.com/blogs/jon_skeet/archive/2007/07/27/smart-enumerations.aspx
// namespace MiscUtil.Collections
/*--------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace ApexParser.Toolbox
{
    /// <summary>
    /// Type chaining an IEnumerable{T} to allow the iterating code
    /// to detect the first and last entries simply.
    /// </summary>
    /// <typeparam name="T">Type to iterate over</typeparam>
    ////[CoverageExclude]
    public class SmartEnumerable<T> : IEnumerable<SmartEnumerable<T>.Entry>
    {
        /// <summary>
        /// Enumerable we proxy to.
        /// </summary>
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartEnumerable{T}"/> class.
        /// </summary>
        /// <param name="enumerable">Collection to enumerate. Must not be null.</param>
        public SmartEnumerable(IEnumerable<T> enumerable) => this.enumerable = enumerable.EmptyIfNull();

        /// <summary>
        /// Returns an enumeration of Entry objects, each of which knows
        /// whether it is the first/last of the enumeration, as well as the
        /// current value.
        /// </summary>
        public IEnumerator<Entry> GetEnumerator()
        {
            using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    yield break;
                }

                bool isFirst = true;
                bool isLast = false;
                int index = 0;

                while (!isLast)
                {
                    T current = enumerator.Current;
                    isLast = !enumerator.MoveNext();
                    yield return new Entry(isFirst, isLast, current, index++);
                    isFirst = false;
                }
            }
        }

        /// <summary>
        /// Non-generic form of GetEnumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Represents each entry returned within a collection,
        /// containing the value and whether it is the first and/or
        /// the last entry in the collection's. enumeration
        /// </summary>
        ////[CoverageExclude]
        public class Entry
        {
            private readonly bool isFirst;
            private readonly bool isLast;
            private readonly T value;
            private readonly int index;

            internal Entry(bool isFirst, bool isLast, T value, int index)
            {
                this.isFirst = isFirst;
                this.isLast = isLast;
                this.value = value;
                this.index = index;
            }

            /// <summary>
            /// Gets the value of the entry.
            /// </summary>
            public T Value => value;

            /// <summary>
            /// Gets a value indicating whether or not this entry is first in the collection's enumeration.
            /// </summary>
            public bool IsFirst => isFirst;

            /// <summary>
            /// Gets a value indicating whether or not this entry is last in the collection's enumeration.
            /// </summary>
            public bool IsLast => isLast;

            /// <summary>
            /// Gets the zero-based index of this entry (i.e. how many entries have been returned before this one)
            /// </summary>
            public int Index => index;
        }
    }
}
