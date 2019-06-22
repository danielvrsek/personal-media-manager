using System;
using System.Collections.Generic;
using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder.Model;
using System.Linq;
using Services.TorrentFilenameParser;
using Vrsek.Common.Utils;

namespace Services.TorrentFinder
{
    public class TorrentMatchCalcutationService : ITorrentMatchCalcutationService
	{
		private readonly ITorrentFilenameParserService _filenameParser;

		public TorrentMatchCalcutationService(ITorrentFilenameParserService filenameParser)
		{
			this._filenameParser = filenameParser;
		}

		public Match<ParsedMedia, TorrentSearchResult> GetBestMatch(ParsedMedia searchedMedia, List<TorrentSearchResult> searchResults)
		{
			return GetMatches(searchedMedia, searchResults)
				.OrderByDescending(match => match.Score)
				.FirstOrDefault();
		}

		public IEnumerable<Match<ParsedMedia, TorrentSearchResult>> GetMatches(ParsedMedia searchedMedia, List<TorrentSearchResult> searchResults)
		{
			foreach (TorrentSiteSearchResult searchResult in searchResults)
			{
				ParsedMedia searchResultParsed = _filenameParser.Parse(searchResult.Name);
				double matchScore = GetMatchScore(searchedMedia, searchResultParsed);

				if (matchScore > 0.2)
				{
					yield return new Match<ParsedMedia, TorrentSearchResult>
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
