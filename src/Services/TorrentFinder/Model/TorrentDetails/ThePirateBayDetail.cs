using Services.TorrentFinder.Common.Attributes;
using System.Collections.Generic;

namespace Services.TorrentFinder.Model
{
	public class ThePirateBayDetail : TorrentSiteDetail
	{
		public ThePirateBayDetail() : base(SiteProvider.ThePirateBay)
		{

		}

		[UrlDecodeFilter]
		[PropertyPattern(@"//div[@class = 'download']//a[contains(@href, 'magnet:?')]")]
		public override IEnumerable<string> MagnetLinks { get; set; }

		[ParseIgnore]
		public override IEnumerable<string> TorrentLinks { get; set; }
	}
}