using Newtonsoft.Json;

namespace MovieDatabaseApi.Model.SearchResults
{
	public enum MediaType
	{
		[JsonProperty("movie")]
		Movie,

		[JsonProperty("tv")]
		TV,

		[JsonProperty("person")]
		Person
	}
}
