using System;
using System.Collections.Generic;
using System.Text;
using TorrentFinder.Common;
using TorrentFinder.Model;
using TorrentFinder.SiteParsers;

namespace TorrentFinder
{
    public interface ITorrentFinderService
    {
		List<TorrentSiteSearchResult> GetSearchResultsFromSite(Site site, string query, int numberOfResults = 10);

		List<TorrentSearchResult> GetSearchResults(ITorrentSearchResultsParser torrentSearchResultParser, SearchUriBuilder searchUriBuilder, string query, int numberOfResults = 10);

		TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, string relativeUrl);

		TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, TorrentSearchResult searchResult);

		TorrentSiteDetail GetTorrentDetail(TorrentSiteSearchResult searchResult);
	}
}
