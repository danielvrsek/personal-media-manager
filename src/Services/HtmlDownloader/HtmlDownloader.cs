using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.HtmlDownloader
{
	public class HtmlDownloaderService : IHtmlDownloaderService
	{
		public async Task<string> DownloadHtmlAsync(Uri uri)
		{
			Contract.Requires(uri != null);

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage response = await client.GetAsync(uri))
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						throw new WebException($"Error while sending the request. Url: {uri.OriginalString}, Status: {response.StatusCode}");
					}

					using (HttpContent content = response.Content)
					{
						return await content.ReadAsStringAsync();
					}
				}
			}
		}

		public string DownloadHtml(Uri uri)
		{
			Contract.Requires(uri != null);

			return DownloadHtmlAsync(uri).Result;
		}
	}
}
