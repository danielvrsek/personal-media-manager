using System;
using System.Collections.Generic;
using TorrentFinder.Common;
using TorrentFinder.Model;

namespace TorrentFinder.SiteParsers
{
	public interface ITorrentSiteParser : ITorrentSearchResultsParser, ITorrentDetailParser
    {
	}
}
