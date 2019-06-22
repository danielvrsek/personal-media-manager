using Services.TorrentFinder.Common.Attributes;
using System.Collections.Generic;

namespace Services.TorrentFinder.Model
{
	public class LeedXDetail : TorrentSiteDetail
    {
		public LeedXDetail() : base(SiteProvider.LeedX)
		{

		}

		[UrlDecodeFilter]
		[PropertyPattern(@"//a[starts-with(@href, 'magnet:?')]/@href")]
		public override IEnumerable<string> MagnetLinks { get; set; }

		[PropertyPattern(@"//a[(substring(@href, string-length(@href) - string-length('.torrent') + 1) = '.torrent') or contains(@href, 'torrent.php')]/@href")]
		public override IEnumerable<string> TorrentLinks { get; set; }
	}
}
