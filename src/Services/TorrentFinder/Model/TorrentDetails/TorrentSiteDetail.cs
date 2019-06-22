using Services.TorrentFinder.Common.Attributes;

namespace Services.TorrentFinder.Model
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
