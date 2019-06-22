using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TorrentFinder.Common
{
	public class SearchUriBuilder : ParameterizedUriBuilder
	{
		public const string Query = "{query}";
		public const string SiteNumber = "{siteNumber}";

		public SearchUriBuilder(string uriString) : base(uriString, new[] { Query, SiteNumber })
		{

		}

		public Uri GetUriWithParameters(string query, int siteNumber = 1)
		{
			string formatedString = OriginalString.Replace(Query, WebUtility.UrlEncode(query)).Replace(SiteNumber, siteNumber.ToString());
			return new Uri(formatedString);
		}
	}

}
