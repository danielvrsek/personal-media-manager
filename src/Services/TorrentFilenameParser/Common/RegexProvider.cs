using Services.TorrentFilenameParser.Model;
using System.Text.RegularExpressions;

namespace Services.TorrentFilenameParser.Common
{
	internal static class RegexProvider
    {
		public static Regex GetInstance(RegexPattern type)
		{
			return new Regex(type.GetPattern());
		}
    }
}
