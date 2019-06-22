using MovieDatabaseApi.Model.DetailResults;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MovieDatabaseApi.Common
{
	internal static class ApiRequestTypeExtensions
	{
		/// <summary>
		/// Gets the UriString from attribute <see cref="UriAttribute"/> of the <see cref="ApiRequestType"/> values
		/// </summary>
		/// <returns>UriString from the <see cref="UriAttribute"/></returns>
		public static string GetUriString(this ApiRequestType value)
		{
			return GetUriAttribute(value).UriString;
		}

		/// <summary>
		/// Gets the UriString from the attribute <see cref="UriAttribute"/> of the <see cref="ApiRequestType"/> values and replaces path parameters
		/// </summary>
		/// <param name="_pathParameters">Path parameters to replace from the <see cref="UriAttribute.UriString"/></param>
		/// <returns>UriString from the <see cref="UriAttribute"/> with replaced path parameters specified in _pathParameters</returns>
		public static string GetUriString(this ApiRequestType value, Dictionary<string, string> _pathParameters)
		{
			StringBuilder uriStringBuilder = new StringBuilder(value.GetUriString());

			foreach (KeyValuePair<string, string> kvp in _pathParameters)
			{
				uriStringBuilder.Replace(kvp.Key, kvp.Value);
			}

			return uriStringBuilder.ToString();
		}

		private static UriAttribute GetUriAttribute(ApiRequestType value)
		{
			var type = value.GetType();
			var name = Enum.GetName(type, value);

			return type.GetField(name).GetCustomAttribute<UriAttribute>();
		}
	}
}
