using MovieDatabaseApi.Common;
using System;

namespace MovieDatabaseApi.Model.DetailResults
{
	internal enum ApiRequestType
	{
		[Uri("https://api.themoviedb.org/3/search/multi")]
		SearchMulti,

		[Uri("https://api.themoviedb.org/3/search/movie")]
		SearchMovie,

		[Uri("https://api.themoviedb.org/3/search/tv")]
		SearchTV,

		[Uri("https://api.themoviedb.org/3/search/person")]
		SearchPeople,

		[Uri("https://api.themoviedb.org/3/movie/{id}")]
		MovieDetail,

		[Uri("https://api.themoviedb.org/3/tv/{id}")]
		TVDetail
	}
}
