using System;

namespace TorrentFinder.Common
{
	/// <summary>
	/// Corresponds with one search result on a site
	/// </summary>
	internal class ItemPatternAttribute : PatternAttribute
	{
		public ItemPatternAttribute(string pattern) : base(pattern)
		{

		}
    }

	/// <summary>
	/// Corresponds with one property of the item (search result)
	/// </summary>
	internal class PropertyPatternAttribute : PatternAttribute
	{
		public PropertyPatternAttribute(string pattern) : base(pattern)
		{

		}
	}

	internal abstract class PatternAttribute : Attribute
	{
		public string Pattern { get; }

		protected PatternAttribute(string pattern)
		{
			Pattern = pattern;
		}
	}
}
