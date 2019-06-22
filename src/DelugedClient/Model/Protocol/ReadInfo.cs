using System.IO;

namespace DelugedClient.Model.Protocol
{
	public class ReadInfo
	{
		public byte[] Buffer { get; set; }
		public Stream Stream { get; set; }

		public ReadInfo(byte[] buffer, Stream stream)
		{
			Buffer = buffer;
			Stream = stream;
		}
	}
}
