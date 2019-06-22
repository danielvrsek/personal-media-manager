using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieDatabaseApi.Model.DetailResults
{
	public abstract class MediaDetail : DetailResult
    {
		[JsonProperty("homepage")]
		public string Homepage { get; set; }

		[JsonProperty("original_title")]
		public virtual string OriginalTitle { get; set; }

		[JsonProperty("popularity")]
		public double Popularity { get; set; }

		[JsonProperty("release_date")]
		public virtual DateTime ReleaseDate { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("belongs_to_collection")]
		public Collection Collection { get; set; }

		[JsonProperty("genres")]
		public List<Genre> Genres { get; set; }

		[JsonProperty("production_companies")]
		public List<ProductionCompany> ProductionCompanies { get; set; }

		[JsonProperty("title")]
		public virtual string Title { get; set; }
	}

	public class Collection
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("poster_path")]
		public string PosterPath { get; set; }

		[JsonProperty("backdrop_path")]
		public string BackdropPath { get; set; }
	}

	public class Genre
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}

	public class ProductionCompany
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }
	}
}
