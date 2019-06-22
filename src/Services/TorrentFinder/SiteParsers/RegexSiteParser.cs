using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.Common.Attributes;
using Services.TorrentFinder.Common.Extensions;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vrsek.Common.Extensions;
using Vrsek.Common.Utils;

namespace Services.TorrentFinder.SiteParsers
{
	/// <summary>
	///
	/// </summary>
	public class RegexSiteParser<TSearch, TDetail> : SiteParserBase<TSearch, TDetail>
			where TSearch : TorrentSearchResult, new()
			where TDetail : TorrentDetail, new()
	{
		public RegexSiteParser(IHtmlDownloaderService htmlDownloaderService)
			: base(htmlDownloaderService)
		{
		}

		protected override IEnumerable<string> GetItemRawResults(string contents, string pattern)
		{
			return RegexParseHelper.GetMatchValues(contents, pattern);
		}

		protected override object GetPropertyValue(Type type, string itemContents, string pattern)
		{
			switch (type)
			{
				case var t when type == typeof(string):
					return RegexParseHelper.GetSingleMatchString(itemContents, pattern);

				case var t when type == typeof(int):
					return RegexParseHelper.GetSingleMatchInt(itemContents, pattern);

				default:
					throw new ArgumentException($"Type {type.Name} is not supported. " +
						"See the list of supported types of properties for more information.");
			}
		}
	}
}