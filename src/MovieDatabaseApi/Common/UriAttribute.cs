using System;

namespace MovieDatabaseApi.Common
{
	internal class UriAttribute : Attribute
    {
		public string UriString { get; set; }

		public UriAttribute(string uriString)
		{
			this.UriString = uriString;
		}
    }
}
