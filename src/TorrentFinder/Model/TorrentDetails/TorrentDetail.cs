using System;
using System.Reflection;
using TorrentFinder.Common;

namespace TorrentFinder.Model
{
	public abstract class TorrentDetail
    {
		public abstract string MagnetLink { get; set; }

		public abstract string TorrentLink { get; set; }

	}
}
