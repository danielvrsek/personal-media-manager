using System;
using System.Collections.Generic;
using TorrentFinder.Common;
using TorrentFinder.SiteParsers;

namespace TorrentFinder.Model
{
	public class SiteProvider
	{
		public static Dictionary<string, Site> KnownSites { get; } = new Dictionary<string, Site>();

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

		public static Site LeedX
		{
			get
			{
				string id = nameof(LeedX);

				if (!KnownSites.TryGetValue(id, out Site leedX))
				{
					Uri baseUri = new Uri("https://1337x.to/");
					SearchUriBuilder searchUri = new SearchUriBuilder($"https://1337x.to/search/{SearchUriBuilder.Query}/{SearchUriBuilder.SiteNumber}/");
					ITorrentSiteParser parser = new RegexSiteParser<LeedXSearchResult, LeedXDetail>();

					leedX = new Site(id, baseUri, searchUri, parser);
					KnownSites.Add(id, leedX);
				}

				return leedX;
			}
		}

		public static Site ThePirateBay
		{
			get
			{
				string id = nameof(ThePirateBay);

				if (!KnownSites.TryGetValue(id, out Site thePirateBay))
				{
					Uri baseUri = new Uri("https://thepiratebay.org/");
					SearchUriBuilder searchUri = new SearchUriBuilder($"https://thepiratebay.org/search/{SearchUriBuilder.Query}/{SearchUriBuilder.SiteNumber}/");
					ITorrentSiteParser parser = new RegexSiteParser<ThePirateBaySearchResult, ThePirateBayDetail>();

					thePirateBay = new Site(id, baseUri, searchUri, parser);
					KnownSites.Add(id, thePirateBay);
				}

				return thePirateBay;
			}
		}
	}
}
