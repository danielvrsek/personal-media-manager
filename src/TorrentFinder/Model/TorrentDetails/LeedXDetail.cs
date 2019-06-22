using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	public class LeedXDetail : TorrentSiteDetail
    {
		public LeedXDetail() : base(SiteProvider.LeedX)
		{

		}

		[PropertyPattern(@"href=""(magnet:\?.+?)""")]
		public override string MagnetLink { get; set; }

		[PropertyPattern(@"href=""(.+?(?:\/[Tt]orrent\/.+?|\/torrent.php\?.+?|\.torrent))""")]
		public override string TorrentLink { get; set; }
	}
}
