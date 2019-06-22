using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.Common.Attributes;
using Services.TorrentFinder.Common.Extensions;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Services.TorrentFinder.SiteParsers
{
    public abstract class SiteParserBase<TSearch, TDetail> : ITorrentSiteParser
		where TSearch : TorrentSearchResult, new()
		where TDetail : TorrentDetail, new()
	{
		private readonly IHtmlDownloaderService _htmlDownloaderService;

		protected SiteParserBase(IHtmlDownloaderService htmlDownloaderService)
		{
			_htmlDownloaderService = htmlDownloaderService;
		}

		/// <summary>
		/// Index of the first searched site specified in the URI
		/// </summary>
		public int SearchStartNumber { get; set; }

		public List<TorrentSearchResult> GetSearchResults(SearchUriBuilder searchUriBuilder, string query, int numberOfResults)
		{
			List<TorrentSearchResult> searchResults = new List<TorrentSearchResult>();
			int siteNumber = SearchStartNumber;

			while (searchResults.Count < numberOfResults)
			{
				IEnumerable<TorrentSearchResult> newResults = GetResultsFromSite(searchUriBuilder, query, siteNumber++);

				if (newResults.Count() <= 0)
				{
					// We are not getting any results
					break;
				}

				searchResults.AddRange(newResults);
			}

			return searchResults.Take(numberOfResults).ToList();
		}

		protected IEnumerable<TorrentSearchResult> GetResultsFromSite(SearchUriBuilder searchUriBuilder, string query, int siteNumber)
		{
			Type searchResultType = typeof(TSearch);
			Uri searchUri = searchUriBuilder.GetUriWithParameters(query, siteNumber);

			string contents = _htmlDownloaderService.DownloadHtml(searchUri);
			string pattern = searchResultType.GetCustomAttribute<ItemPatternAttribute>().Pattern;

			// List of single items (item = raw search result (row) from the response)
			IEnumerable<string> rawResults = GetItemRawResults(contents, pattern);

			foreach (string rawResult in rawResults)
			{
				TSearch searchResult = new TSearch();
				SetModelPropertiesValues(rawResult, searchResult);

				yield return searchResult;
			}
		}

		public TorrentDetail GetDetailModel(Uri detailUri)
		{
			string contents = _htmlDownloaderService.DownloadHtml(detailUri);

			TDetail detail = new TDetail();
			SetModelPropertiesValues(contents, detail);

			return detail;
		}

		protected void SetModelPropertiesValues<TModel>(string contents, TModel modelInstance)
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

				object propertyValue = GetPropertyValue(propertyInfo.PropertyType, contents, pattern);

				FilterAttribute filterAttribute = propertyInfo.GetCustomAttribute<FilterAttribute>();
				if (filterAttribute != null)
				{
					Type originalType = propertyValue.GetType();
					propertyValue = filterAttribute.OnValueSetting(propertyValue, originalType);
					Type returnedType = propertyValue.GetType();

					/*if (!returnedType.IsAssignableFrom(originalType))
					{
						throw new InvalidOperationException($"Original type ({originalType.Name}) must match with the returned type from {nameof(FilterAttribute)}. Returned type was {returnedType.Name}.");
					}*/
				}

				propertyInfo.SetValue(modelInstance, propertyValue);
			}
		}

		protected abstract IEnumerable<string> GetItemRawResults(string contents, string pattern);

		protected abstract object GetPropertyValue(Type type, string itemContents, string pattern);
	}
}