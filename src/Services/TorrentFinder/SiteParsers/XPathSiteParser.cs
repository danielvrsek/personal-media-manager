using HtmlAgilityPack;
using Services.HtmlDownloader;
using Services.TorrentFinder.Common;
using Services.TorrentFinder.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.TorrentFinder.SiteParsers
{
	public class XPathSiteParser<TSearch, TDetail> : SiteParserBase<TSearch, TDetail>
		where TSearch : TorrentSearchResult, new()
		where TDetail : TorrentDetail, new()
	{
		public XPathSiteParser(IHtmlDownloaderService htmlDownloaderService)
			: base(htmlDownloaderService)
		{
		}

		protected override IEnumerable<string> GetItemRawResults(string contents, string pattern)
		{
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(contents);

			return doc.DocumentNode.SelectNodes(pattern).Select(item => item.InnerHtml);
		}

		protected override object GetPropertyValue(Type type, string itemContents, string pattern)
		{
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(itemContents);

			return GetPropertyValue(type, doc.DocumentNode, pattern);
		}

		protected object GetPropertyValue(Type type, HtmlNode node, string pattern)
		{
			switch (type)
			{
				case var t when type == typeof(string):
					return GetStringFromHtmlNode(node, pattern);

				case var t when type == typeof(int):
					return GetIntFromHtmlNode(node, pattern);

				case var t when typeof(IEnumerable<string>) == type:
					return GetArrayFromHtmlNode<string>(node, pattern);

				case var t when typeof(IEnumerable<int>) == type:
					return GetArrayFromHtmlNode<int>(node, pattern);

				default:
					throw new ArgumentException($"Type {type.Name} is not supported. " +
						"See the list of supported types of properties for more information.");
			}
		}

		private IEnumerable<T> GetArrayFromHtmlNode<T>(HtmlNode docNode, string pattern)
		{
			string attributeName = GetAttributeName(pattern);
			attributeName = attributeName != null ? "/@" + attributeName : String.Empty;

			return docNode.SelectNodes(pattern)
				.Select(node => GetPropertyValue(typeof(T), node.ParentNode, "*" + attributeName))
				.Cast<T>();
		}

		private string GetStringFromHtmlNode(HtmlNode node, string pattern)
		{
			HtmlNode stringNode = node.SelectSingleNode(pattern);
			string attributeName = GetAttributeName(pattern);

			return attributeName != null ? stringNode.Attributes[attributeName].Value : stringNode.InnerText;
		}

		private int GetIntFromHtmlNode(HtmlNode node, string pattern)
		{
			return Int32.Parse(GetStringFromHtmlNode(node, pattern));
		}

		private string GetAttributeName(string input)
		{
			Match match = Regex.Match(input, @"\/@(.+)$");

			return match.Success ? match.Groups[1].Value : null;
		}
	}
}