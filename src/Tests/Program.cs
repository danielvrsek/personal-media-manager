using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.AutomaticTorrentDownloader;
using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tests.Attributes;

namespace Tests
{
	class Program
	{
		/*private static ITorrentFinderService _torrentFinder = new TorrentFinderService();
		private static ITorrentFilenameParserService _torrentFilenameParser = new TorrentFilenameParserService();
		private static ITorrentMatchCalcutationService _torrentMatchCalculator = new TorrentMatchCalcutationService(_torrentFilenameParser);
		private static IAutomaticTorrentDownloaderService _automaticTorrentDownloader = new AutomaticTorrentDownloaderService(_torrentFinder, _torrentMatchCalculator);*/

		private static IServiceProvider serviceProvider;
		private static IConfiguration configuration;

		static void Main(string[] args)
		{
			configuration = GetConfiguration();
			Startup startup = new Startup(configuration);

			IServiceCollection services = new ServiceCollection();
			services.AddSingleton(configuration);
			services.AddSingleton(services);
			startup.ConfigureServices(services);

			Type testingClass = GetTestingClass();
			Console.Write('\n');
			MethodInfo testingMethod = GetTestingMethod(testingClass);
			services.AddTransient(testingClass);
			serviceProvider = services.BuildServiceProvider();

			StartTesting(testingMethod);
		}

		private static void StartTesting(MethodInfo testingMethod)
		{
			Console.Clear();
			Console.WriteLine("Spousti se testovani...");

			object instance = serviceProvider.GetService(testingMethod.DeclaringType);
			testingMethod.Invoke(instance, null);

			Console.WriteLine("Uspesne dokonceno.");
			Console.Read();
		}

		private static Type GetTestingClass()
		{
			Type[] testClasses = Assembly.GetExecutingAssembly().GetTypes()
				.Where(classType => classType.GetCustomAttributes<TestClassAttribute>().Any())
				.ToArray();
			int index = GetChoiceIndex(testClasses.Select(testClass => testClass.Name));

			return testClasses[index];
		}

		private static MethodInfo GetTestingMethod(Type type)
		{
			MethodInfo[] testMethods = type.GetMethods()
				.Where(method => method.GetCustomAttributes<TestMethodAttribute>().Any())
				.ToArray();
			int index = GetChoiceIndex(testMethods.Select(testMethod => testMethod.Name));

			return testMethods[index];
		}

		private static int GetChoiceIndex(IEnumerable<string> choices)
		{
			int choicesCount = choices.Count();

			if (choicesCount == 0)
					throw new Exception("There are no choices.");
			else if (choicesCount == 1)
					return 0;

			int index = 0;
			string choicesText = index++ + " - " + choices.Aggregate((current, next) =>
			{
				return $"{current}\n{index++} - {next}";
			});

			while (true)
			{
				Console.WriteLine(choicesText);

				string choice = Console.ReadLine();

				try
				{
					int choiceIndex = Convert.ToInt32(choice);
					return choiceIndex < choicesCount ? choiceIndex : throw new Exception();
				}
				catch
				{
					Console.WriteLine("Chybne zadany vstup.\n****************************\n\n");
				}
			}
		}

		private static IConfiguration GetConfiguration()
		{
			return new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", false, true)
				.Build();
		}

		public static void ConfigurationTest()
		{
			AutomaticTorrentDownloaderConfiguration configuration = new AutomaticTorrentDownloaderConfiguration(true)
			{
				RefreshDelayMinutes = 30,
				SearchedMedia = new List<ParsedMedia>
				{
					new ParsedMedia
					{
						Title = "Lord of the rings",
						Resolution = "1080p"
					}
				},
				SearchedSites = new List<string>
				{
					SiteProvider.LeedX.Id,
					SiteProvider.ThePirateBay.Id
				}
			};
			configuration.Save();

			AutomaticTorrentDownloaderConfiguration test = new AutomaticTorrentDownloaderConfiguration(true);
			Console.WriteLine(test.RefreshDelayMinutes);
		}
	}
}