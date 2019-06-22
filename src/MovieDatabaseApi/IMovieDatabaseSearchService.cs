using MovieDatabaseApi.Model.DetailResults;
using MovieDatabaseApi.Model.SearchResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieDatabaseApi
{
	public interface IMovieDatabaseSearchService
	{
		MediaDetail GetItemDetail(MediaType mediaType, int id);

		Task<List<MediaSearchResult>> SearchMultiByQueryAsync(string query);

		List<MediaSearchResult> SearchMultiByQuery(string query);
	}
}