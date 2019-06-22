using System;

namespace Services.TorrentFilenameParser.Common
{
	internal class PatternAttribute : Attribute
    {
		public string Pattern { get; private set; }

		public PatternAttribute(string pattern)
		{
			this.Pattern = pattern;
		}
    }
}
