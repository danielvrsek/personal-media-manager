using MovieDatabaseApi.Common;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.DetailResults
{
	[ApiRequestType(ApiRequestType.MovieDetail)]
	public class MovieDetail : MediaDetail
	{
		[JsonProperty("adult")]
		public bool Adult { get; }

		[JsonProperty("budget")]
		public int Budget { get; }

		[JsonProperty("imdb_id")]
		public string ImdbId { get; }

		[JsonProperty("runtime")]
		public int Runtime { get; }

		[JsonProperty("production_countries")]
		public List<ProductionCountry> ProductionCountries { get; set; }

		[JsonProperty("revenue")]
		public int Revenue { get; set; }

		[JsonProperty("spoken_languages")]
		public List<SpokenLanguage> SpokenLanguage { get; set; }

		[JsonProperty("tagline")]
		public string Tagline { get; set; }
	}

	public class ProductionCountry
	{
		[JsonProperty("iso_3166_1")]
		public string Code { get; set; }
		
		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class SpokenLanguage
	{
		[JsonProperty("iso_639_1")]
		public string Code { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
