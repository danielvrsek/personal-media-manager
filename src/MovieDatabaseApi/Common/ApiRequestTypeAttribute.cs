using MovieDatabaseApi.Model.DetailResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabaseApi.Common
{
	internal class ApiRequestTypeAttribute : Attribute
	{
		public ApiRequestType RequestType { get; }

		public ApiRequestTypeAttribute(ApiRequestType requestType)
		{
			this.RequestType = requestType;
		}
	}
}
