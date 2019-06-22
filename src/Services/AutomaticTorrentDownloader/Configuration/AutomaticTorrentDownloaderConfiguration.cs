using Services.TorrentFilenameParser.Model;
using System.Collections.Generic;
using Vrsek.Common.Configuration;

namespace Services.AutomaticTorrentDownloader
{
	public class AutomaticTorrentDownloaderConfiguration : Configuration
	{
		public AutomaticTorrentDownloaderConfiguration()
		{

		}

		public AutomaticTorrentDownloaderConfiguration(bool load) : base(load)
		{

		}

		protected override string FileName { get; } = "AutomaticTorrentDownloaderConfiguration.xml";

		public int RefreshDelayMinutes { get; set; }

		public List<ParsedMedia> SearchedMedia { get; set; }

		public List<string> SearchedSites { get; set; }
	}
}
