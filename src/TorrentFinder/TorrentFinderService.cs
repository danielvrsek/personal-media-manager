using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using TorrentFinder.Common;
using TorrentFinder.Model;
using TorrentFinder.SiteParsers;
using Vrsek.Common.Extensions;

namespace TorrentFinder
{
	public class TorrentFinderService : ITorrentFinderService
	{
		public List<TorrentSiteSearchResult> GetSearchResultsFromSite(Site site, string query, int numberOfResults = 10)
		{
			return GetSearchResults(site.Parser, site.SearchUri, query, numberOfResults).Cast<TorrentSiteSearchResult>().ToList();
		}

		public List<TorrentSearchResult> GetSearchResults(ITorrentSearchResultsParser torrentSearchResultParser, SearchUriBuilder searchUriBuilder, string query, int numberOfResults = 10)
		{
			return torrentSearchResultParser.GetSearchResults(searchUriBuilder, query, numberOfResults, DownloadHtml);
		}

		public TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, TorrentSearchResult searchResult)
		{
			searchResult.TorrentDetail = GetTorrentDetail(torrentDetailParser, baseUri, searchResult.RelativeDetailUrl);
			return searchResult.TorrentDetail;
		}

		public TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, string relativeUrl)
		{
			return torrentDetailParser.GetDetailModel(new Uri(baseUri, relativeUrl), DownloadHtml);
		}

		public TorrentSiteDetail GetTorrentDetail(TorrentSiteSearchResult searchResult)
		{
			Site site = searchResult.Site;

			return GetTorrentDetail(site.Parser, site.BaseUri, searchResult) as TorrentSiteDetail;
		}

		protected virtual async Task<string> DownloadHtmlAsync(Uri uri)
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage response = await client.GetAsync(uri))
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						throw new WebException("Error while sending the request. Url: " + uri.OriginalString);
					}

					using (HttpContent content = response.Content)
					{
						return await content.ReadAsStringAsync();
					}
				}
			}
		}

		protected virtual string DownloadHtml(Uri uri)
		{
			return DownloadHtmlAsync(uri).Result;
		}
	}
}
