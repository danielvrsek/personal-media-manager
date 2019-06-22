using DelugedClient.DelugeConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieDatabaseApi;
using Services.AutomaticTorrentDownloader;
using Services.Cache;
using Services.TorrentFilenameParser;
using Services.TorrentFinder;
using System;

namespace Tests
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddSingleton<ICacheService, CacheService>();
			services.AddTransient<IAutomaticTorrentDownloaderService, AutomaticTorrentDownloaderService>();
			services.AddTransient<ITorrentFinderService, TorrentFinderService>();
			services.AddTransient<ITorrentFilenameParserService, TorrentFilenameParserService>();
			ConfigureMovieDatabaseApi(services);
			ConfigureDelugedClient(services);
		}

		public void ConfigureDelugedClient(IServiceCollection services)
		{
			string hostname = Configuration["AppConfiguration:DelugedClient:Connection:Hostname"] ?? throw new ArgumentNullException(nameof(hostname));
			int port = Convert.ToInt32(Configuration["AppConfiguration:DelugedClient:Connection:Port"]);
			string login = Configuration["AppConfiguration:DelugedClient:Connection:Account:Login"] ?? throw new ArgumentNullException(nameof(login));
			string password = Configuration["AppConfiguration:DelugedClient:Connection:Account:Password"] ?? throw new ArgumentNullException(nameof(password));

			services.AddSingleton<IDelugedConnectionService>(serviceProvider => DelugedConnectionService.GetConnection(hostname, port));
		}

		public void ConfigureMovieDatabaseApi(IServiceCollection services)
		{
			string apiKey = Configuration["AppConfiguration:MovieDatabaseApi:ApiKey"] ?? throw new ArgumentNullException(nameof(apiKey));

			services.AddTransient<IMovieDatabaseSearchService>(serviceProvider => new MovieDatabaseSearchService(apiKey));
		}
	}
}
