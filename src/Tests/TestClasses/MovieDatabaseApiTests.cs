using MovieDatabaseApi;
using MovieDatabaseApi.Model.DetailResults;
using MovieDatabaseApi.Model.SearchResults;
using Services.TorrentFilenameParser;
using Services.TorrentFilenameParser.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Attributes;

namespace Tests.TestClasses
{
	[TestClass]
    public class MovieDatabaseApiTests
	{
		private readonly ITorrentFilenameParserService _filenameParser;
		private readonly IMovieDatabaseSearchService _apiSearchService;

		public MovieDatabaseApiTests(
			ITorrentFilenameParserService filenameParser,
			IMovieDatabaseSearchService apiSearchService)
		{
			this._filenameParser = filenameParser;
			this._apiSearchService = apiSearchService;
		}

		[TestMethod]
		public void ApiTest()
		{
			ParsedMedia parsedMedia = _filenameParser.Parse("The.Secret.Life.of.Walter.Mitty.2013.1080p.BluRay.x264.YIFY");

			MediaSearchResult searchResult = _apiSearchService.SearchMultiByQuery(parsedMedia.Title)[0];
			MediaDetail apiResult = _apiSearchService.GetItemDetail(searchResult.MediaType, searchResult.Id);

			Console.WriteLine(apiResult);
		}
	}
}
