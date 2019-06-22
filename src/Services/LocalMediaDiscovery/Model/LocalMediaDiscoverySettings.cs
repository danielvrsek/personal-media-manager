using System;
using System.Collections.Generic;
using System.Text;

namespace Services.LocalMediaDiscovery.Model
{
    public class LocalMediaDiscoverySettings
    {
		public bool Recursive { get; set; } = true;

		public List<string> Extensions { get; set; } = new List<string>() { ".mp4", ".avi", ".mkv", ".mov" };

		public string ExcludeByPattern { get; set; } = @"(?i:(?:sample|hddevils\.bap|\\etrg\.mp4))";
    }
}
