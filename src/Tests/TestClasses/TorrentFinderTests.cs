using Services.TorrentFilenameParser;
using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Attributes;

namespace Tests.TestClasses
{
	[TestClass]
    public class TorrentFinderTests
    {
		private readonly ITorrentFilenameParserService _filenameParser;
		private readonly ITorrentFinderService _torrentFinder;

		public TorrentFinderTests(
			ITorrentFilenameParserService filenameParser,
			ITorrentFinderService torrentFinder)
		{
			this._filenameParser = filenameParser;
			this._torrentFinder = torrentFinder;
		}

		[TestMethod]
		public void TorrentSearchTest()
		{
			ParsedMedia parsedMedia = _filenameParser.Parse("The.Secret.Life.of.Walter.Mitty.2013.1080p.BluRay.x264.YIFY");
			Site leedX = SiteProvider.LeedX;
			IEnumerable<ParsedMedia> searchResults = _torrentFinder.GetSearchResultsFromSite(SiteProvider.ThePirateBay, $"{parsedMedia.Title} {parsedMedia.Year}", 10)
				.Select(result => _filenameParser.Parse(result.Name));
			
			//TorrentSiteDetail torrentDetail = TorrentFinderService.GetTorrentDetail(searchResults[0]);

			foreach (ParsedMedia searchResult in searchResults)
			{
				Console.WriteLine(searchResult);
			}
		}
	}
}
