using System.Globalization;

namespace MovieDatabaseApi.Common
{
	public class SearchSettings
    {
		public CultureInfo CultureInfo { get; set; } = new CultureInfo("en-US");
    }
}
