using System;
using System.Collections.Generic;
using System.Text;
using TorrentFinder.Model;
using Services.TorrentFilenameParser.Model;

namespace TorrentFinder
{
    public class TorrentMatchCalcutationService
    {
		private Match<ParsedMedia, TorrentSiteSearchResult> GetBestMatch(ParsedMedia searchedMedia, List<TorrentSiteSearchResult> searchResults)
		{
			return GetMatches(searchedMedia, searchResults)
				.OrderByDescending(match => match.Score)
				.FirstOrDefault();
		}

		private IEnumerable<Match<ParsedMedia, TorrentSiteSearchResult>> GetMatches(ParsedMedia searchedMedia, List<TorrentSiteSearchResult> searchResults)
		{
			foreach (TorrentSiteSearchResult searchResult in searchResults)
			{
				ParsedMedia searchResultParsed = TorrentFilenameParserService.Parse(searchResult.Name);
				double matchScore = GetMatchScore(searchedMedia, searchResultParsed);

				if (matchScore > 0.2)
				{
					yield return new Match<ParsedMedia, TorrentSiteSearchResult>
					{
						Source = searchedMedia,
						Target = searchResult,
						Score = matchScore
					};
				}
			}
		}


		private double GetMatchScore(ParsedMedia searchedMedia, ParsedMedia parsedSearchResult)
		{
			double matchScore = 1.0;

			var maximalDistances = new[]
			{
				new { Name = nameof(ParsedMedia.Title), Ratio = 0.2 },
				new { Name = nameof(ParsedMedia.Season), Ratio = 0.0 },
				new { Name = nameof(ParsedMedia.Episode), Ratio = 0.0 },
				new { Name = nameof(ParsedMedia.Quality), Ratio = 0.5 },
				new { Name = nameof(ParsedMedia.Resolution), Ratio = 0.0 }
			};

			Type type = typeof(ParsedMedia);

			foreach (var maximalDistance in maximalDistances)
			{
				string searchedMediaPropertyValue = type.GetProperty(maximalDistance.Name).GetValue(searchedMedia) as string;
				string searchResultPropertyValue = type.GetProperty(maximalDistance.Name).GetValue(parsedSearchResult) as string;

				if (!LevenshteinDistance.CheckLevenshteinDistanceRatio(searchedMediaPropertyValue, searchResultPropertyValue, maximalDistance.Ratio))
				{
					matchScore -= 1.0 - maximalDistance.Ratio;
				}
			}

			return matchScore;
		}
	}
}
