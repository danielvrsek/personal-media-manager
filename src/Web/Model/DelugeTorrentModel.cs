using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class DelugeTorrentModel
    {
		public string TorrentId { get; set; }

		public string Name { get; set; }

		public int Seeds { get; set; }

		public int Peers { get; set; }

		public string Size { get; set; }

		public string DownloadSpeed { get; set; }

		public string UploadSpeed { get; set; }

		public decimal Progress { get; set; }
    }
}
