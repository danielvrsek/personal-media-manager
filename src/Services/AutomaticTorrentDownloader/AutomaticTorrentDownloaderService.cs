using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder;
using Services.TorrentFinder.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vrsek.Common;

namespace Services.AutomaticTorrentDownloader
{
	public class AutomaticTorrentDownloaderService : HostedService, IAutomaticTorrentDownloaderService
	{
		private readonly ITorrentFinderService _torrentFinder;
		private readonly ITorrentMatchCalcutationService _matchCalculator;

		public AutomaticTorrentDownloaderService(
			ITorrentFinderService torrentFinder,
			ITorrentMatchCalcutationService matchCalculator)
		{
			this._torrentFinder = torrentFinder;
			this._matchCalculator = matchCalculator;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			AutomaticTorrentDownloaderConfiguration configuration = new AutomaticTorrentDownloaderConfiguration();

			while (!cancellationToken.IsCancellationRequested)
			{
				configuration.Load();

				foreach (Site site in configuration.SearchedSites.Select(id => SiteProvider.GetById(id)))
				{
					foreach (ParsedMedia searchedMedia in configuration.SearchedMedia)
					{
						string query = GetSearchQuery(searchedMedia);
						var searchResults = _torrentFinder.GetSearchResultsFromSite(site, query);
						var match = _matchCalculator.GetBestMatch(searchedMedia, searchResults);

						if (match == null || match.Score < 0.5)
						{
							continue;
						}
					}
				}
				
				await Task.Delay(TimeSpan.FromMinutes(configuration.RefreshDelayMinutes), cancellationToken);
			}
		}

		

		/// <summary>
		/// Get's the query string for searching for the <see cref="ParsedMedia"/> on various web sites
		/// </summary>
		/// <param name="parsedMedia"><see cref="ParsedMedia"/> that is going to be used to create query string.</param>
		/// <returns>Query string that can be used for searching</returns>
		private string GetSearchQuery(ParsedMedia parsedMedia)
		{
			if (String.IsNullOrWhiteSpace(parsedMedia.Title))
			{
				throw new ArgumentNullException("parsedMedia.Title", "Title can not be null, empty or whitespace.");
			}

			StringBuilder queryBuilder = new StringBuilder(parsedMedia.Title.Trim());

			if (!String.IsNullOrWhiteSpace(parsedMedia.Season) && !String.IsNullOrWhiteSpace(parsedMedia.Episode))
			{
				AppendIfNotNull($"S{parsedMedia.Season}E{parsedMedia.Episode}");
			}
			else
			{
				AppendIfNotNull(parsedMedia.Year);
			}

			AppendIfNotNull(parsedMedia.Quality);
			AppendIfNotNull(parsedMedia.Resolution);
			AppendIfNotNull(parsedMedia.Group);

			return queryBuilder.ToString();

			bool AppendIfNotNull(string text)
			{
				if (String.IsNullOrWhiteSpace(text))
				{
					return false;
				}

				text = text.Trim();

				queryBuilder.Append(' ');
				queryBuilder.Append(text);
				return true;
			}

			/*void AppendFirstNotNull(params string[] texts)
			{
				for (int i = 0; i < texts.Length && !AppendIfNotNull(texts[i]); i++) ;
			}*/
		}
	}
}
