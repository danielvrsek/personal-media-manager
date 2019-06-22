using Services.LocalMediaDiscovery;
using Services.LocalMediaDiscovery.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Attributes;

namespace Tests.TestClasses
{
	[TestClass]
    public class LocalMediaDiscoveryTests
	{
		[TestMethod]
		public void DiscoveryTest()
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			LocalMediaDiscoveryService mediaDiscovery = new LocalMediaDiscoveryService(@"M:\Movies");

			foreach (LocalMediaFile mediaFile in mediaDiscovery.GetFiles())
			{
				Console.WriteLine(mediaFile.FileName);
			}
		}
	}
}
