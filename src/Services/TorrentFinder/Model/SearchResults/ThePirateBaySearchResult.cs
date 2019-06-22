using Services.TorrentFinder.Common.Attributes;

namespace Services.TorrentFinder.Model
{
	[ItemPattern(@"//table[@id = 'searchResult']/tr")]
    public class ThePirateBaySearchResult : TorrentSiteSearchResult
    {
		public ThePirateBaySearchResult() : base(SiteProvider.ThePirateBay)
		{

		}

		[PropertyPattern(@".//a[@class = 'detLink']/text()")]
		public override string Name { get; set; }

		[RegexFilter(@"Size (.+?),")]
		[PropertyPattern(@".//font[@class = 'detDesc']/text()")]
		public override string Size { get; set; }

		[PropertyPattern(@"td[3]/text()")]
		public override int Seeds { get; set; }

		[PropertyPattern(@"td[4]/text()")]
		public override int Leeches { get; set; }

		[RegexFilter(@"Uploaded (.+?),")]
		[PropertyPattern(@".//font[@class = 'detDesc']/text()")]
		public override string Uploaded { get; set; }

		[PropertyPattern(@".//a[@class = 'detLink']/@href")]
		public override string RelativeDetailUrl { get; set; }
	}
}
