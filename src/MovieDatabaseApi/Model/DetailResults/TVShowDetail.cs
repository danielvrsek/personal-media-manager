using MovieDatabaseApi.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.DetailResults
{
	[ApiRequestType(ApiRequestType.TVDetail)]
    public class TVShowDetail : MediaDetail
    {
		[JsonProperty("created_by")]
		public List<CreatedBy> CreatedBy { get; set; }

		[JsonProperty("episode_run_time")]
		public List<int> EpisodeRuntime { get; set; }

		[JsonProperty("first_air_date")]
		public override DateTime ReleaseDate { get; set; }

		[JsonProperty("in_production")]
		public bool InPriduction { get; set; }

		[JsonProperty("languages")]
		public List<string> Languages { get; set; }

		[JsonProperty("last_air_date")]
		public DateTime LastAirDate { get; set; }

		[JsonProperty("name")]
		public override string Title { get; set; }

		[JsonProperty("networks")]
		public List<Network> Networks { get; set; }

		[JsonProperty("number_of_episodes")]
		public int NumberOfEpisodes { get; set; }

		[JsonProperty("number_of_seasons")]
		public int NumberOfSeasons { get; set; }

		[JsonProperty("origin_country")]
		public List<string> OriginCountry { get; set; }

		[JsonProperty("original_name")]
		public override string OriginalTitle { get; set; }

		[JsonProperty("seasons")]
		public List<Season> Seasons { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public class CreatedBy
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("profile_path")]
		public string PrifilePath { get; set; }
	}

	public class Network
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class Season
	{
		[JsonProperty("air_date")]
		public DateTime AirDate { get; set; }

		[JsonProperty("episode_count")]
		public int EpisodeCount { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("poster_path")]
		public object PosterPath { get; set; }

		[JsonProperty("season_number")]
		public int SeasonNumber { get; set; }
	}
}
