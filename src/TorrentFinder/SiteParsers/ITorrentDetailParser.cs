using System;
using System.Collections.Generic;
using System.Text;
using TorrentFinder.Model;

namespace TorrentFinder.SiteParsers
{
    public interface ITorrentDetailParser
    {
		TorrentDetail GetDetailModel(Uri detailUri, Func<Uri, string> downloadHtml);
	}
}
