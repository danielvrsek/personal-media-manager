using MovieDatabaseApi.Model.DetailResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.SearchResults
{
	public class SearchResult : DetailResult
	{
		[JsonProperty("original_name")]
		public string OriginalName { get; set; }

		[JsonProperty("media_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public MediaType MediaType { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("first_air_date")]
		public DateTime FirstAirDate { get; set; }

		[JsonProperty("genre_ids")]
		public List<int> Genres { get; set; }

		[JsonProperty("origin_country")]
		public List<string> OriginCountries { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("original_title")]
		public string OriginalTitle { get; set; }

		[JsonProperty("release_date")]
		public DateTime ReleaseDate { get; set; }

	}
}
