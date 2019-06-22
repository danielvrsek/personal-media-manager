using Services.TorrentFinder.Common.Attributes;

namespace Services.TorrentFinder.Model
{
	[ItemPattern(@"//table[contains(@class, 'table-list')]/tbody/tr")]
	public class LeedXSearchResult : TorrentSiteSearchResult
	{
		public LeedXSearchResult() : base(SiteProvider.LeedX)
		{

		}

		[PropertyPattern(@".//td[contains(@class, 'name')]/a[2]/text()")]
		public override string Name { get; set; }

		[PropertyPattern(@".//td[contains(@class, 'size')]/text()")]
		public override string Size { get; set; }

		[PropertyPattern(@".//td[contains(@class, 'seeds')]/text()")]
		public override int Seeds { get; set; }

		[PropertyPattern(@".//td[contains(@class, 'leeches')]/text()")]
		public override int Leeches { get; set; }

		[PropertyPattern(@".//td[contains(@class, '-date')]/text()")]
		public override string Uploaded { get; set; }

		[PropertyPattern(@".//td[contains(@class, 'name')]/a[2]/@href")]
		public override string RelativeDetailUrl { get; set; }
	}
}