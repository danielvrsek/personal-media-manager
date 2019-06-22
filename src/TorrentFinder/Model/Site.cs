using System;
using TorrentFinder.Common;
using TorrentFinder.SiteParsers;

namespace TorrentFinder.Model
{
	public class Site
	{
		public string Id { get; }

		public Uri BaseUri { get; }

		public SearchUriBuilder SearchUri { get; }

		public ITorrentSiteParser Parser { get; }

		public Site(string id, Uri baseUri, SearchUriBuilder searchUri, ITorrentSiteParser parser)
		{
			this.Id = id;
			this.BaseUri = baseUri;
			this.SearchUri = searchUri;
			this.Parser = parser;
		}
	}

}
