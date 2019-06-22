using System;
using System.Collections.Generic;
using System.Text;

namespace Services.TorrentFinder.Model
{
	public class Match<T, T2>
	{
		public T Source { get; set; }

		public T2 Target { get; set; }

		public double Score { get; set; }
	}
}
