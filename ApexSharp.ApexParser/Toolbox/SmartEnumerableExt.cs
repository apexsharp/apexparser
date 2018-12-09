// SmartEnumerable by Jon Skeet
// http://msmvps.com/blogs/jon_skeet/archive/2007/07/27/smart-enumerations.aspx
// namespace MiscUtil.Collections
/*--------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;

namespace ApexSharp.ApexParser.Toolbox
{
	/// <summary>
	/// Wrapper methods for SmartEnumerable[T].
	/// </summary>
	////[CoverageExclude]
	public static class SmartEnumerableExt
	{
		/// <summary>
		/// Extension method to make life easier.
		/// </summary>
		/// <typeparam name="T">Type of enumerable</typeparam>
		/// <param name="source">Source enumerable</param>
		/// <returns>A new SmartEnumerable of the appropriate type</returns>
		public static SmartEnumerable<T> AsSmartEnumerable<T>(this IEnumerable<T> source)
		{
			return new SmartEnumerable<T>(source);
		}
	}
}
