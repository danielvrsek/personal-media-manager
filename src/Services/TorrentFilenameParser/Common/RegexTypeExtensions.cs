using Services.TorrentFilenameParser.Model;
using System;
using System.Reflection;

namespace Services.TorrentFilenameParser.Common
{
	internal static class RegexTypeExtensions
	{
		internal static string GetPattern(this RegexPattern value)
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);

			PatternAttribute attribute = type.GetField(name).GetCustomAttribute<PatternAttribute>();

			return attribute.Pattern;
		}
	}
}
