using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	public class ThePirateBayDetail : TorrentSiteDetail
	{
		public ThePirateBayDetail() : base(SiteProvider.ThePirateBay)
		{

		}

		[PropertyPattern(@"(?s:<dt>Spoken language\(s\):<\/dt>.+?<dd>(.+?)<\/dd>)")]
		public string Language { get; set; }

		[ParseIgnore]
		public override string TorrentLink { get; set; }
		
		[PropertyPattern(@"href=""(magnet:\?.+?)""")]
		public override string MagnetLink { get; set; }
	}
}