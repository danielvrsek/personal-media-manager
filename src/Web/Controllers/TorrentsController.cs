using DelugedClient.DelugeConnection;
using DelugedClient.Model;
using Microsoft.AspNetCore.Mvc;
using Services.Cache;
using Services.TorrentFinder;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Filters;
using Web.Model;

namespace Web.Controllers
{
	[HandleException]
	[Route("api/[controller]/[action]")]
    public class TorrentsController : Controller
    {
		private readonly ITorrentFinderService _torrentFinder;
		private readonly IDelugedConnectionService _delugedConnection;

		public TorrentsController(
			ITorrentFinderService torrentFinder,
			IDelugedConnectionService delugedConnection)
		{
			_torrentFinder = torrentFinder;
			_delugedConnection = delugedConnection;
		}

		public EnumerableResultModel<SearchResultModel> Search(string query)
		{
			var searchResults = _torrentFinder.GetSearchResultsFromSite(SiteProvider.LeedX, query, 10);

			return new EnumerableResultModel<SearchResultModel>
			{
				Results = searchResults
					.OfType<TorrentSiteSearchResult>()
					.Select(result => new SearchResultModel
					{
						SiteId = result.Site.Id,
						Name = result.Name,
						Size = result.Size,
						Seeds = result.Seeds,
						Leeches = result.Leeches,
						Uploaded = result.Uploaded,
						RelativeDetailUrl = result.RelativeDetailUrl
					})
			};
		}

		public async Task<DownloadRequestResultModel> Download(string siteId, string relativeUrl)
		{
			Site site = SiteProvider.GetById(siteId) ?? throw new ArgumentException("Unknown site: " + siteId);

			TorrentDetail detail = _torrentFinder.GetTorrentDetail(site, relativeUrl);
			Torrent t = await _delugedConnection.Daemon.AddTorrentMagnetAsync(detail.MagnetLinks.ElementAt(0));

			return new DownloadRequestResultModel();
		}

		public async Task<LoginResultModel> Login()
		{
			return new LoginResultModel
			{
				IsSuccess = await _delugedConnection.Daemon.LoginAsync("daniel", "Eldorado12")
			};
		}

		public async Task<GetAllResultModel> GetAll()
		{
			var torrents = await _delugedConnection.Daemon.GetTorrentsStatusAsync(TorrentStatusKey.NumberOfSeeds | TorrentStatusKey.NumberOfPeers | TorrentStatusKey.DownloadSpeed
																			| TorrentStatusKey.UploadSpeed | TorrentStatusKey.Name | TorrentStatusKey.Progress | TorrentStatusKey.TotalSize);

			return new GetAllResultModel
			{
				Results = torrents
				.Select(t => new Torrent(t))
					.Select(torrent =>
						new DelugeTorrentModel
						{
							TorrentId = torrent.TorrentId,
							Name = torrent[TorrentStatusKey.Name].ToString(),
							Seeds = (int)torrent[TorrentStatusKey.NumberOfSeeds],
							Peers = (int)torrent[TorrentStatusKey.NumberOfPeers],
							Size = Math.Round((getNumber(torrent[TorrentStatusKey.TotalSize]) ?? 0) / 1024 / 1024, 2) + " MiB",
							DownloadSpeed = Math.Round((getNumber(torrent[TorrentStatusKey.DownloadSpeed]) ?? 0) / 1024, 2) + " KiB/s",
							UploadSpeed = Math.Round((getNumber(torrent[TorrentStatusKey.UploadSpeed]) ?? 0) / 1024, 2) + " KiB/s",
							Progress = Math.Round(getNumber(torrent[TorrentStatusKey.Progress]) ?? 0, 2)
						})
			};

			decimal? getNumber(object o)
			{
				decimal? number = o as int?;

				if (number != null) return number;
		
				number = o as long?;

				if (number != null)	return number;

				float? fnumber = o as float?;

				if (fnumber != null) return (decimal)fnumber;

				return o as decimal?;
			}
		}
	}
}
