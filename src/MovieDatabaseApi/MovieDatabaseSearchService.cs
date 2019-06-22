using MovieDatabaseApi.Common;
using MovieDatabaseApi.Model.DetailResults;
using MovieDatabaseApi.Model.SearchResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MovieDatabaseApi
{
	/// <summary>
	/// Service for searching media on https://www.themoviedb.org/
	/// </summary>
	public class MovieDatabaseSearchService : IMovieDatabaseSearchService
	{
		public class Parameters
		{
			public const string Id = "{id}";
		}

		private readonly string _apiKey;
		private readonly JsonSerializerSettings _jsonSettings;

		public SearchSettings Settings { get; } = new SearchSettings();

		/// <summary>
		/// Creates new instance of the <see cref="MovieDatabaseSearchService"/> with the specified api key
		/// </summary>
		/// <param name="apiKey">Api key is unique identifier of the app. You can get api key from https://www.themoviedb.org/</param>
		public MovieDatabaseSearchService(string apiKey)
		{
			_apiKey = apiKey;
			_jsonSettings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd", NullValueHandling = NullValueHandling.Ignore };
		}

		/// <summary>
		/// Creates new instance of the <see cref="MovieDatabaseSearchService"/> with the specified api key and settings
		/// </summary>
		/// <param name="apiKey">Api key is unique identifier of the app. You can get api key from https://www.themoviedb.org/</param>
		/// <param name="settings">Settings to use with the request</param>
		public MovieDatabaseSearchService(string apiKey, SearchSettings settings) : this(apiKey)
		{
			Settings = settings;
		}

		private async Task<TModel> GetItemDetailAsync<TModel>(int id) 
		{
			ApiRequestTypeAttribute attribute = GetAttribute<ApiRequestTypeAttribute>(typeof(TModel));

			ApiRequestType requestType = attribute.RequestType;

			if (!requestType.GetUriString().Contains(Parameters.Id))
			{
				throw new ArgumentException("This Model requires ID.");
			}

			MovieDatabaseApiRequest request = new MovieDatabaseApiRequest(_apiKey, requestType);
			request.AddQueryParameter("language", Settings.CultureInfo.TwoLetterISOLanguageName);
			request.AddPathParameter(Parameters.Id, id.ToString());

			string responseString = await request.GetResponseAsync();

			return JsonConvert.DeserializeObject<TModel>(responseString, _jsonSettings);
		}

		private TModel GetItemDetail<TModel>(int id)
		{
			return GetItemDetailAsync<TModel>(id).Result;
		}

		/// <summary>
		/// Gets the instance of the detail model parsed from the Api depending on the <see cref="MediaType"/> parameter value and id
		/// </summary>
		/// <param name="mediaType">Parameter that specifies which media type to search for</param>
		/// <param name="id">Id for the detail request specified in the search response</param>
		/// <returns>New instance of the <see cref="MediaDetail"/>. It can be either <see cref="MovieDetail"/> or <see cref="TVShowDetail"/></returns>
		public MediaDetail GetItemDetail(MediaType mediaType, int id)
		{
			switch (mediaType)
			{
				case MediaType.Movie:
					return GetItemDetail<MovieDetail>(id);
				case MediaType.TV:
					return GetItemDetail<TVShowDetail>(id);
				default:
					return null;
			}
		}

		/// <summary>
		/// Searches asynchronously Api for the query. Returns <see cref="MediaSearchResult"/> that contains ID required to get a detail model of the media.
		/// </summary>
		/// <param name="query">Search query</param>
		/// <returns><see cref="MediaSearchResult"/> that contains ID required to get a detail model of the media.</returns>
		public async Task<List<MediaSearchResult>> SearchMultiByQueryAsync(string query)
		{
			List<MediaSearchResult> mediaList = new List<MediaSearchResult>();

			MovieDatabaseApiRequest request = new MovieDatabaseApiRequest(_apiKey, ApiRequestType.SearchMulti);
			request.AddQueryParameter("query", query);
			request.AddQueryParameter("language", Settings.CultureInfo.TwoLetterISOLanguageName);

			string responseString = await request.GetResponseAsync();
			SearchResponse response = JsonConvert.DeserializeObject<SearchResponse>(responseString, _jsonSettings);

			foreach (SearchResult result in response.Results)
			{
				if (result.MediaType != MediaType.Person)
				{
					mediaList.Add(result);
				}
			}

			return mediaList;
		}

		/// <summary>
		/// Searches Api for the query. Returns <see cref="MediaSearchResult"/> that contains ID required to get a detail model of the media.
		/// </summary>
		/// <param name="query">Search query</param>
		/// <returns><see cref="MediaSearchResult"/> that contains ID required to get a detail model of the media.</returns>
		public List<MediaSearchResult> SearchMultiByQuery(string query)
		{
			return SearchMultiByQueryAsync(query).Result;
		}

		private static TAttribute GetAttribute<TAttribute>(Type type) where TAttribute : Attribute
		{
			TAttribute attribute = type.GetTypeInfo().GetCustomAttribute<TAttribute>();

			if (attribute == null)
			{
				throw new InvalidOperationException($@"Class ""{type.GetTypeInfo().Name}"" does not contain requested attribute.");
			}
			return attribute;
		}
	}
}
