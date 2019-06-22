using MovieDatabaseApi.Common;
using MovieDatabaseApi.Model.DetailResults;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabaseApi
{

	/// <summary>
	/// New request to Media Api. Request uri is specified as an attribute for each value of the <see cref="ApiRequestType"/>
	/// </summary>
	internal class MovieDatabaseApiRequest
    {
		private readonly string _apiKey;

		private readonly Dictionary<string, string> _queryParameters = new Dictionary<string, string>();

		private readonly Dictionary<string, string> _pathParameters = new Dictionary<string, string>();

		private Uri RequestUri
		{
			get
			{
				string uriString = RequestType.GetUriString(_pathParameters);

				return new Uri(uriString + GetSerializedQueryParameters());
			}
		}

		public ApiRequestType RequestType { get; }

		public MovieDatabaseApiRequest(string apiKey, ApiRequestType requestType)
		{
			_apiKey = apiKey;
			RequestType = requestType;

			AddQueryParameter("api_key", _apiKey);
		}


		/// <summary>
		/// Adds new query parameter to this request.
		/// <para>Query parameter in uri is for example http://foo.com/path?queryParameterKey=queryParameterValue</para>
		/// </summary>
		/// <param name="key">Query parameter name</param>
		/// <param name="value">Query parameter key</param>
		public void AddQueryParameter(string key, string value)
		{
			_queryParameters.Add(WebUtility.UrlEncode(key), WebUtility.UrlEncode(value));
		}

		/// <summary>
		/// Adds new path parameter to this request.
		/// <para>Path parameter in uri is for example http://foo.com/{pathParameter}/{pathParameter2}</para>
		/// </summary>
		/// <param name="key">Path parameter key. This represents {pathParameter} and this value will be replaced</param>
		/// <param name="value">Path parameter value. This represents value the key will be replaced for</param>
		public void AddPathParameter(string key, string value)
		{
			_pathParameters.Add(key, WebUtility.UrlEncode(value));
		}

		/// <summary>
		/// Gets url-serialized query parameters stored in <see cref="MovieDatabaseApiRequest._queryParameters"/>
		/// </summary>
		/// <returns>Url-serialized parameters</returns>
		private string GetSerializedQueryParameters()
		{
			StringBuilder builder = new StringBuilder();

			using (var enumerator = _queryParameters.GetEnumerator())
			{
				int index = 0;
				while (enumerator.MoveNext())
				{
					builder.Append(index++ == 0 ? '?' : '&');
					builder.Append($"{enumerator.Current.Key}={enumerator.Current.Value}");
				}
			}

			return builder.ToString();
		}


		/// <summary>
		/// Gets the asynchronous response for the instantiated <see cref="MovieDatabaseApiRequest"/>
		/// </summary>
		/// <returns>Asynchronous response from the api</returns>
		public async Task<string> GetResponseAsync()
		{
			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage response = await client.GetAsync(RequestUri))
				{
					if (response.StatusCode != HttpStatusCode.OK)
					{
						throw new WebException("Error while sending the request. Url: " + RequestUri.OriginalString);
					}

					using (HttpContent content = response.Content)
					{
						return await content.ReadAsStringAsync();
					}
				}
			}
		}

		/// <summary>
		/// Gets the response for the instantiated <see cref="MovieDatabaseApiRequest"/>
		/// </summary>
		/// <returns>Response from the api</returns>
		public string GetResponse()
		{
			return GetResponseAsync().Result;
		}
	}
}
