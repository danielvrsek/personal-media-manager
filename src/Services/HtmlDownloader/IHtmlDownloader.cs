using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.HtmlDownloader
{
    public interface IHtmlDownloaderService
    {
		Task<string> DownloadHtmlAsync(Uri uri);

		string DownloadHtml(Uri uri);
	}
}
