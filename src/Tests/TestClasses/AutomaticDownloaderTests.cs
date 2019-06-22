using Services.AutomaticTorrentDownloader;
using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Attributes;

namespace Tests.TestClasses
{
	[TestClass]
    public class AutomaticDownloaderTests
    {
		[TestMethod]
		public static void AutomaticTorrentDownloaderTest()
		{
			AutomaticTorrentDownloaderConfiguration configuration = new AutomaticTorrentDownloaderConfiguration(true)
			{
				RefreshDelayMinutes = 30,
				SearchedMedia = new List<ParsedMedia>
				{
					new ParsedMedia
					{
						Title = "Lord of the rings",
						Resolution = "1080p"
					}
				},
				SearchedSites = new List<string>
				{
					SiteProvider.ThePirateBay.Id
				}
			};
			configuration.Save();
		}
	}
}
