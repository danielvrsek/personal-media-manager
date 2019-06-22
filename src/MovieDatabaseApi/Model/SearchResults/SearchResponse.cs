using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.SearchResults
{
	public class SearchResponse
    {
		[JsonProperty("page")]
		public int Page { get; set; }
		[JsonProperty("total_results")]
		public int TotalResults { get; set; }
		[JsonProperty("total_pages")]
		public int TotalPages { get; set; }
		[JsonProperty("results")]
		public List<SearchResult> Results { get; set; }
	}
}
