using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TorrentFinder.Common;
using TorrentFinder.Model;
using Vrsek.Common.Extensions;
using Vrsek.Common.Utils;

namespace TorrentFinder.SiteParsers
{
	/// <summary>
	/// 
	/// </summary>
	public class RegexSiteParser<TSearch, TDetail> : ITorrentSiteParser
			where TSearch : TorrentSearchResult, new()
			where TDetail : TorrentDetail, new()
	{
		/// <summary>
		/// Index of the first searched site specified in the URI
		/// </summary>
		public int SearchStartNumber { get; set; } = 0;

		/// <summary>
		/// <para>Searches for <see cref="TorrentSiteSearchResult"/> for the <paramref name="query"/> on <see cref="Site"/> specified while creating new instance of this class.</para>
		/// </summary>
		/// <param name="query">Search query</param>
		/// <param name="numberOfResults">Number of <see cref="TorrentSiteSearchResult"/> to be returned</param>
		/// <param name="downloadHtml">Function provided to download page</param>
		/// <returns></returns>
		public List<TorrentSearchResult> GetSearchResults(SearchUriBuilder searchUriBuilder, string query, int numberOfResults, Func<Uri, string> downloadHtml)
		{
			List<TorrentSearchResult> searchResults = new List<TorrentSearchResult>();
			int siteNumber = SearchStartNumber;

			int previousNumberOfResults = 0;
			while (searchResults.Count < numberOfResults)
			{
				searchResults.AddRange(GetResultsFromSite(searchUriBuilder, query, siteNumber++, downloadHtml));

				if (searchResults.Count <= previousNumberOfResults)
				{
					// We are not getting any results
					break;
				}

				previousNumberOfResults = searchResults.Count;
			}

			return searchResults.Take(numberOfResults).ToList();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="detailUri"></param>
		/// <param name="downloadHtml"></param>
		/// <returns></returns>
		public TorrentDetail GetDetailModel(Uri detailUri, Func<Uri, string> downloadHtml)
		{
			string contents = downloadHtml(detailUri);

			TDetail detail = new TDetail();
			SetModelPropertiesValues(contents, detail);

			return detail;
		}

		protected IEnumerable<TorrentSearchResult> GetResultsFromSite(SearchUriBuilder searchUriBuilder, string query, int siteNumber, Func<Uri, string> downloadHtml) 
		{
			Type searchResultType = typeof(TSearch);
			Uri searchUri = searchUriBuilder.GetUriWithParameters(query, siteNumber);

			string contents = downloadHtml(searchUri);

			// List of single items (item = raw search result (row) from the response) 
			IEnumerable<string> rawResults = RegexParseHelper.GetMatchValues(contents, searchResultType.GetCustomAttribute<ItemPatternAttribute>().Pattern);

			foreach (string rawResult in rawResults)
			{
				TSearch searchResult = new TSearch();
				SetModelPropertiesValues(rawResult, searchResult);

				yield return searchResult;
			}
		}

		private void SetModelPropertiesValues<TModel>(string contents, TModel modelInstance)
		{
			Type modelType = modelInstance.GetType();
			PropertyInfo[] modelProperties = modelType.GetProperties();

			string pattern;

			foreach (PropertyInfo propertyInfo in modelProperties)
			{
				if ((pattern = propertyInfo.GetPropertyPattern()) == null)
				{
					continue;
				}

				switch (propertyInfo.PropertyType)
				{
					case var type when type == typeof(string):
						propertyInfo.SetValue(modelInstance, RegexParseHelper.GetSingleMatchString(contents, pattern));
						break;
					case var type when type == typeof(int):
						propertyInfo.SetValue(modelInstance, RegexParseHelper.GetSingleMatchInt(contents, pattern));
						break;
					default:
						throw new ArgumentException($"Type {propertyInfo.PropertyType.Name} of the value {propertyInfo.Name} is not supported. " +
							"See the list of supported types of properties for more information.");
				}
			}
		}
	}
}
