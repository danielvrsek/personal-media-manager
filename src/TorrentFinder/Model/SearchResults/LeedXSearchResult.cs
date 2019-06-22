using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	[ItemPattern(@"(?s:(<tr>.<td.*?><a.*?<\/tr>))")]
    public class LeedXSearchResult : TorrentSiteSearchResult
    {
		public LeedXSearchResult() : base(SiteProvider.LeedX)
		{

		}

		[PropertyPattern(@"<td class=.+name.+>(?:<a href="".+?"">(.+?)<\/a>)")]
		public override string Name { get; set; }

		[PropertyPattern(@"<td class=.+size.+?>(.+?)<span")]
		public override string Size { get; set; }

		[PropertyPattern(@"<td class="".+? seeds"">(.+?)<\/td>")]
		public override int Seeds { get; set; }

		[PropertyPattern(@"<td class="".+? leeches"">(.+?)<\/td>")]
		public override int Leeches { get; set; }

		[PropertyPattern(@"<td class="".+?-date"">(.+?)<\/td>")]
		public override string Uploaded { get; set; }

		[PropertyPattern(@"<td class=.+ name.+>(?:<a href=""(.+?)"">.+?<\/a>)")]
		public override string RelativeDetailUrl { get; set; }
	}
}
