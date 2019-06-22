using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class SearchResultModel : RequestResult
    {
		public string SiteId { get; set; }

		public string Name { get; set; }

		public string Size { get; set; }

		public int Seeds { get; set; }

		public int Leeches { get; set; }

		public string Uploaded { get; set; }

		public string RelativeDetailUrl { get; set; }
	}
}
