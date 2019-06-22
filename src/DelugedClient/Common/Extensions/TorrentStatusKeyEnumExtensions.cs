using DelugedClient.Model;
using Vrsek.Common.Extensions;

namespace DelugedClient.Common
{
	public static class TorrentStatusKeyEnumExtensions
    {
		public static string GetStatusKeyName(this TorrentStatusKey value)
		{
			return value.GetCustomAttribute<StatusKeyNameAttribute>().Name;
		}
	}
}
