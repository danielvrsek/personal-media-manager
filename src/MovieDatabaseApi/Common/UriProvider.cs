using MovieDatabaseApi.Model.DetailResults;
using System;

namespace MovieDatabaseApi.Common
{
	internal static class UriProvider
	{
		public static Uri GetInstance(ApiRequestType requestType)
		{
			return new Uri(requestType.GetUriString());
		}
    }
}
