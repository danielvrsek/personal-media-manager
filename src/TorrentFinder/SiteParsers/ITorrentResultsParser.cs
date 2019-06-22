using System;
using System.Collections.Generic;
using System.Text;
using TorrentFinder.Common;
using TorrentFinder.Model;

namespace TorrentFinder.SiteParsers
{
    public interface ITorrentSearchResultsParser
    {
		List<TorrentSearchResult> GetSearchResults(SearchUriBuilder searchUriBuilder, string query, int numberOfResults, Func<Uri, string> downloadHtml);
	}
}
