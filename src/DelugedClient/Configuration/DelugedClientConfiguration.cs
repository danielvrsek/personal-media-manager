using Vrsek.Common.Configuration;

namespace DelugedClient
{
	internal class DelugedClientConfiguration : Configuration
	{
		protected override string FileName => "DelugedClientConfiguration.xml";

		public string Hostname { get; set; } = "127.0.0.1";

		public int Port { get; set; } = 58846;
	}
}
