using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Services.TorrentFinder.Common.Attributes
{
    public class UrlDecodeFilterAttribute : FilterAttribute
	{
		public override string OnValueSetting(string value)
		{
			return WebUtility.UrlDecode(value);
		}

		public override IEnumerable<string> OnValueSetting(IEnumerable<string> values)
		{
			return values.Select(v => OnValueSetting(v));
		}
	}
}
