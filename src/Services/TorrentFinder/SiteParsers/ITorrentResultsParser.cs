using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;

namespace Services.TorrentFinder.SiteParsers
{
	public interface ITorrentSearchResultsParser
    {
		/// <summary>
		/// <para>Searches for <see cref="TorrentSiteSearchResult"/> for the <paramref name="query"/> on <see cref="Site"/> specified while creating new instance of this class.</para>
		/// </summary>
		/// <param name="query">Search query</param>
		/// <param name="numberOfResults">Number of <see cref="TorrentSiteSearchResult"/> to be returned</param>
		/// <param name="downloadHtml">Function provided to download page</param>
		List<TorrentSearchResult> GetSearchResults(SearchUriBuilder searchUriBuilder, string query, int numberOfResults);
	}
}