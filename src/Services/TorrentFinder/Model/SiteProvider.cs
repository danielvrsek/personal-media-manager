using System;
using System.Collections.Generic;
using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.SiteParsers;

namespace Services.TorrentFinder.Model
{
	public static class SiteProvider
	{
		static SiteProvider()
		{
			KnownSites = new Dictionary<string, Site>();
			KnownSites[LeedX.Id] = LeedX;
			KnownSites[ThePirateBay.Id] = ThePirateBay;
		}

		private static Dictionary<string, Site> KnownSites { get; }

		public static Site GetById(string id)
		{
			try
			{
				return KnownSites[id];
			}
			catch (KeyNotFoundException)
			{
				return null;
			}
		}

		private static Site _leedX;

		public static Site LeedX
		{
			get
			{
				if (_leedX == null)
				{
					string id = nameof(LeedX);
					Uri baseUri = new Uri("https://1337x.to/");
					SearchUriBuilder searchUri = new SearchUriBuilder($"https://1337x.to/search/{SearchUriBuilder.Query}/{SearchUriBuilder.SiteNumber}/");
					ITorrentSiteParser parser = new XPathSiteParser<LeedXSearchResult, LeedXDetail>(new HtmlDownloaderService());

					_leedX = new Site(id, baseUri, searchUri, parser);
				}

				return _leedX;
			}
		}

		private static Site _thePirateBay;

		public static Site ThePirateBay
		{
			get
			{
				if (_thePirateBay == null)
				{
					string id = nameof(ThePirateBay);
					Uri baseUri = new Uri("https://thepiratebay.org/");
					SearchUriBuilder searchUri = new SearchUriBuilder($"https://thepiratebay.org/search/{SearchUriBuilder.Query}/{SearchUriBuilder.SiteNumber}/");
					ITorrentSiteParser parser = new XPathSiteParser<ThePirateBaySearchResult, ThePirateBayDetail>(new HtmlDownloaderService());

					_thePirateBay = new Site(id, baseUri, searchUri, parser);
				}

				return _thePirateBay;
			}
		}
	}
}