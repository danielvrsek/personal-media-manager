using System;
using System.Collections.Generic;
using System.Net;

namespace TorrentFinder.Common
{
	public class ParameterizedUriBuilder
	{
		protected List<string> Parameters { get; } = new List<string>();

		private Uri _uri;

		public string OriginalString { get; }

		public Uri Uri => _uri ?? (_uri = new Uri(OriginalString));

		public ParameterizedUriBuilder(string uriString, IEnumerable<string> parameters)
		{
			OriginalString = uriString;
			Parameters.AddRange(parameters);

			if (!Parameters.TrueForAll(parameter => uriString.Contains(parameter)))
			{
				throw new ArgumentException($"Address must contain required properties - {String.Join(", ", Parameters)}");
			}
		}
	}

}
