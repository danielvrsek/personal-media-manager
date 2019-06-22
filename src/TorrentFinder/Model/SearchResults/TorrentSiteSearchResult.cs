using TorrentFinder.Common;

namespace TorrentFinder.Model
{

	public abstract class TorrentSiteSearchResult : TorrentSearchResult, ITorrentSiteModelBase
	{
		[ParseIgnore]
		public Site Site { get; }

		[ParseIgnore]
		public override TorrentDetail TorrentDetail { get; set; }

		protected TorrentSiteSearchResult(Site site)
		{
			this.Site = site;
		}
	}
}
