using Services.TorrentFinder.Common;
using Services.TorrentFinder.Model;
using Services.TorrentFinder.SiteParsers;
using System;
using System.Collections.Generic;

namespace Services.TorrentFinder
{
	public interface ITorrentFinderService
    {
		List<TorrentSearchResult> GetSearchResultsFromSite(Site site, string query, int numberOfResults = 10);

		List<TorrentSearchResult> GetSearchResults(ITorrentSearchResultsParser torrentSearchResultParser, SearchUriBuilder searchUriBuilder, string query, int numberOfResults = 10);

		TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, string relativeUrl);

		TorrentDetail GetTorrentDetail(Site site, string relativeUrl);

		TorrentDetail GetTorrentDetail(ITorrentDetailParser torrentDetailParser, Uri baseUri, TorrentSearchResult searchResult);

		TorrentDetail GetTorrentDetail(TorrentSiteSearchResult searchResult);
	}
}
