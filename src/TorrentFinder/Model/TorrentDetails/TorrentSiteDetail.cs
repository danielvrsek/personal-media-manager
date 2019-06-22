using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	public abstract class TorrentSiteDetail : TorrentDetail
	{
		[ParseIgnore]
		public Site Site { get; set; }


		public TorrentSiteDetail(Site site)
		{
			Site = site;
		}
	}
}
