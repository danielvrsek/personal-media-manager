using DelugedClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DelugedClient.Model
{
	public class Torrent : Dictionary<TorrentStatusKey, object>
	{
		public string TorrentId { get; }
		public Torrent(string torrentId)
		{
			TorrentId = torrentId ?? throw new Exception("TorrentId cannot be null.");
		}

		public Torrent(KeyValuePair<object, object> torrent)
		{
			TorrentId = (torrent.Key as String) ?? throw new ArgumentException("TorrentId must be valid.");

			var statuses = (torrent.Value as Dictionary<object, object>) ?? throw new ArgumentException("Invalid statuses.");
			var values = GetAllTorrentStatusKeys();

			foreach (TorrentStatusKey statusKey in values)
			{
				if (statuses.TryGetValue(statusKey.GetStatusKeyName(), out object statusValue))
				{
					Add(statusKey, statusValue);
				}
			}
		}

		private IEnumerable<TorrentStatusKey> GetAllTorrentStatusKeys()
		{
			Type type = typeof(TorrentStatusKey);
			return Enum.GetValues(type).Cast<TorrentStatusKey>();
		}
    }
}
