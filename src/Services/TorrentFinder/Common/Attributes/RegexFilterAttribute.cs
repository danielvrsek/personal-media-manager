using System;
using System.Collections.Generic;
using System.Linq;
using Vrsek.Common.Utils;

namespace Services.TorrentFinder.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RegexFilterAttribute : FilterAttribute
    {
		public string Pattern { get; }

		public RegexFilterAttribute(string pattern)
		{
			Pattern = pattern;
		}

		public override string OnValueSetting(string value)
		{
			return RegexParseHelper.GetSingleMatchString(value, Pattern);
		}

		public override IEnumerable<string> OnValueSetting(IEnumerable<string> values)
		{
			return values.Select(v => OnValueSetting(v));
		}
	}
}
