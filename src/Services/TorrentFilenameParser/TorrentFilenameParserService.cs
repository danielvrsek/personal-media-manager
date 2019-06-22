using Services.TorrentFilenameParser.Common;
using Services.TorrentFilenameParser.Model;
using System;
using System.Text.RegularExpressions;

namespace Services.TorrentFilenameParser
{
	public class TorrentFilenameParserService : ITorrentFilenameParserService
	{
		public ParsedMedia Parse(string filename)
		{
			ParsedMedia parsedMedia = new ParsedMedia();
			// First index of parsed data (year, season..)
			int firstIndex = filename.Length - 1;

			parsedMedia.Season = MatchByRegexPattern(RegexPattern.Season, filename, ref firstIndex);
			parsedMedia.Episode = MatchByRegexPattern(RegexPattern.Episode, filename, ref firstIndex);
			parsedMedia.Year = MatchByRegexPattern(RegexPattern.Year, filename, ref firstIndex);
			parsedMedia.Resolution = MatchByRegexPattern(RegexPattern.Resolution, filename, ref firstIndex);
			parsedMedia.Quality = MatchByRegexPattern(RegexPattern.Quality, filename, ref firstIndex);
			parsedMedia.Codec = MatchByRegexPattern(RegexPattern.Codec, filename, ref firstIndex);
			parsedMedia.Audio = MatchByRegexPattern(RegexPattern.Audio, filename, ref firstIndex);
			parsedMedia.Group = MatchByRegexPattern(RegexPattern.Group,  filename, ref firstIndex);
			parsedMedia.Size = MatchByRegexPattern(RegexPattern.Size, filename, ref firstIndex);
			parsedMedia.Title = filename.Remove(firstIndex).Replace('.', ' ').Trim();

			return parsedMedia;
		}

		private string MatchByRegexPattern(RegexPattern pattern, string filename, ref int firstIndex)
		{
			Match match = RegexProvider.GetInstance(pattern).Match(filename);

			Group group = match.Groups[match.Groups.Count - 1];

			if (match.Success)
			{
				//filename = RemoveMatch(group, filename);

				if (firstIndex > match.Index)
				{
					firstIndex = match.Index;
				}
			}

			return !String.IsNullOrWhiteSpace(group.Value) ? group.Value : null;
		}

		private string RemoveMatch(Group match, string filename)
		{
			int index = match.Index;
			int length = match.Length;

			filename = filename.Remove(index, length);

			return filename;
		}
	}
}
