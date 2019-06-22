using DelugedClient.DelugeConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieDatabaseApi;
using Services.AutomaticTorrentDownloader;
using Services.Cache;
using Services.HtmlDownloader;
using Services.TorrentFinder;

namespace Web
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
            services.AddMvc();
			services.AddSingleton<ICacheService, CacheService>();
			services.AddTransient<IAutomaticTorrentDownloaderService, AutomaticTorrentDownloaderService>();
			services.AddTransient<ITorrentFinderService, TorrentFinderService>();
			services.AddTransient<IHtmlDownloaderService, HtmlDownloaderService>();
			ConfigureMovieDatabaseApi(services);
			ConfigureDelugedClient(services);
		}

		public void ConfigureDelugedClient(IServiceCollection services)
		{
			string hostname = Configuration["AppConfiguration:DelugedClient:Connection:Hostname"];
			int port = Configuration.GetValue<int>("AppConfiguration:DelugedClient:Connection:Port");
			string login = Configuration["AppConfiguration:DelugedClient:Connection:Account:Login"];
			string password = Configuration["AppConfiguration:DelugedClient:Connection:Account:Password"];

			services.AddSingleton<IDelugedConnectionService>(serviceProvider => DelugedConnectionService.GetConnection(hostname, port));
		}

		public void ConfigureMovieDatabaseApi(IServiceCollection services)
		{
			string apiKey = Configuration["AppConfiguration:MovieDatabaseApi:ApiKey"];

			services.AddTransient<IMovieDatabaseSearchService>(serviceProvider => new MovieDatabaseSearchService(apiKey));
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
