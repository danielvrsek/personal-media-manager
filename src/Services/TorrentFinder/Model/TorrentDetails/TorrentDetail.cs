using System.Collections.Generic;

namespace Services.TorrentFinder.Model
{
	public abstract class TorrentDetail
    {
		public abstract IEnumerable<string> MagnetLinks { get; set; }

		public abstract IEnumerable<string> TorrentLinks { get; set; }

	}
}
