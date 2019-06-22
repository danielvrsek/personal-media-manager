using System;
using System.Collections.Generic;
using System.Text;
using TorrentFinder.SiteParsers;

namespace TorrentFinder.Model
{
    internal interface ITorrentSiteModelBase
    {
		Site Site { get; }
    }
}
