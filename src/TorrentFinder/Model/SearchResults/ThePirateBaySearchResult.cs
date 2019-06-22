using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	[ItemPattern(@"(?s:(<tr(?! class=""header"")>.+?<\/tr>))")]
    public class ThePirateBaySearchResult : TorrentSiteSearchResult
    {
		public ThePirateBaySearchResult() : base(SiteProvider.ThePirateBay)
		{

		}

		[PropertyPattern(@"<a href=""\/torrent\/.+?"".+?>(.+?)<\/a>")]
		public override string Name { get; set; }

		[PropertyPattern(@"Size (.+?),")]
		public override string Size { get; set; }

		[PropertyPattern(@"(?s:<td align=""right"">(.+?)<\/td>(?!<td align=""right"">).+<\/tr>)")]
		public override int Seeds { get; set; }

		[PropertyPattern(@"(?s:<\/font>.+?(?:.+<td align=""right"">(.+)<\/td>))")]
		public override int Leeches { get; set; }

		[PropertyPattern(@"Uploaded (.+?),")]
		public override string Uploaded { get; set; }

		[PropertyPattern(@"<a href=""(\/torrent\/.+?)""")]
		public override string RelativeDetailUrl { get; set; }
	}
}
