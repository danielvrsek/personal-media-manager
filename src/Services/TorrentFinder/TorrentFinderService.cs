using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.Model;
using Services.TorrentFinder.SiteParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.TorrentFinder
{
	public class TorrentFinderService : ITorrentFinderService
	{
		public List<TorrentSearchResult> GetSearchResults(ITorrentSearchResultsParser torrentSearchResultParser, SearchUriBuilder searchUriBuilder, string query, int numberOfResults = 10)
		{
			Contract.Requires(torrentSearchResultParser != null);
			Contract.Requires(searchUriBuilder != null);
			Contract.Requires(!String.IsNullOrEmpty(query));

			return torrentSearchResultParser.GetSearchResults(searchUriBuilder, query, numberOfResults);
		}

		public List<TorrentSearchResult> GetSearchResultsFromSite(Site site, string query, int numberOfResults = 10)
		{
			Contract.Requires(site != null);
			Contract.Requires(!String.IsNullOrEmpty(query));

			return GetSearchResults(site.Parser, site.SearchUri, query, numberOfResults).ToList();
		}

		public TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, string relativeUrl)
		{
			Contract.Requires(torrentDetailParser != null);
			Contract.Requires(baseUri != null);
			Contract.Requires(!String.IsNullOrEmpty(relativeUrl));

			return torrentDetailParser.GetDetailModel(new Uri(baseUri, relativeUrl));
		}

		public TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, TorrentSearchResult searchResult)
		{
			Contract.Requires(torrentDetailParser != null);
			Contract.Requires(baseUri != null);
			Contract.Requires(searchResult != null);

			searchResult.TorrentDetail = GetTorrentDetail(torrentDetailParser, baseUri, searchResult.RelativeDetailUrl);
			return searchResult.TorrentDetail;
		}

		public TorrentDetail GetTorrentDetail(Site site, string relativeUrl)
		{
			Contract.Requires(site != null);
			Contract.Requires(!String.IsNullOrEmpty(relativeUrl));

			return GetTorrentDetail(site.Parser, site.BaseUri, relativeUrl);
		}

		public TorrentDetail GetTorrentDetail(TorrentSiteSearchResult searchResult)
		{
			Contract.Requires(searchResult != null);

			return GetTorrentDetail(searchResult.Site, searchResult.RelativeDetailUrl);
		}
	}
}