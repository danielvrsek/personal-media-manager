using System;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.SearchResults
{
	public class MediaSearchResult
    {
		public int Id { get; private set; }

		public string Title { get; private set; }

		public string OriginalTitle { get; private set; }

		public DateTime ReleaseDate { get; private set; }

		public int VoteCount { get; private set; }

		public double VoteAverage { get; private set; }

		public string PosterPath { get; private set; }

		public string OriginalLanguage { get; private set; }

		public string BackdropPath { get; private set; }

		public string Overview { get; private set; }

		public List<string> OriginCountries { get; private set; }

		public MediaType MediaType { get; private set; }

		public static implicit operator MediaSearchResult(SearchResult result)
		{
			if (result.MediaType != MediaType.Movie && result.MediaType != MediaType.TV)
			{
				throw new InvalidCastException("SearchResult is not of a type MediaSearchResult.");
			}

			MediaSearchResult mediaResult = new MediaSearchResult();

			mediaResult.Id = result.Id;
			mediaResult.OriginalTitle = result.OriginalTitle ?? result.OriginalName;
			mediaResult.Title = result.Title ?? result.Name;
			mediaResult.VoteCount = result.VoteCount;
			mediaResult.VoteAverage = result.VoteAverage;
			mediaResult.PosterPath = result.PosterPath;
			mediaResult.ReleaseDate = result.ReleaseDate != default(DateTime) ? result.ReleaseDate : result.FirstAirDate;
			mediaResult.OriginalLanguage = result.OriginalLanguage;
			mediaResult.BackdropPath = result.BackdropPath;
			mediaResult.Overview = result.Overview;
			mediaResult.OriginCountries = result.OriginCountries;
			mediaResult.MediaType = result.MediaType;

			return mediaResult;
		}
	}
}
