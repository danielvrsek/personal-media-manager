using Newtonsoft.Json;

namespace MovieDatabaseApi.Model.DetailResults
{
	public abstract class DetailResult
    {
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("vote_count")]
		public int VoteCount { get; set; }

		[JsonProperty("vote_average")]
		public double VoteAverage { get; set; }

		[JsonProperty("poster_path")]
		public string PosterPath { get; set; }

		[JsonProperty("backdrop_path")]
		public string BackdropPath { get; set; }

		[JsonProperty("original_language")]
		public string OriginalLanguage { get; set; }

		[JsonProperty("overview")]
		public string Overview { get; set; }
	}
}
