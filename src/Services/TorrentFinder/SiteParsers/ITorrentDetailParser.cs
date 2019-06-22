using Services.HtmlDownloader;
using Services.TorrentFinder.Model;
using System;

namespace Services.TorrentFinder.SiteParsers
{
	public interface ITorrentDetailParser
    {
		TorrentDetail GetDetailModel(Uri detailUri);
	}
}