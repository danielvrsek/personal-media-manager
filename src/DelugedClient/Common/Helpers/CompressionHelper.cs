using ComponentAce.Compression.Libs.zlib;
using System.IO;

namespace DelugedClient.Common
{
	public class CompressionHelper
	{
		public static string Compress(string decompressed)
		{
			return EncodingHelper.GetString(CompressBytes(EncodingHelper.GetBytes(decompressed)));
		}

		public static string Decompress(string compressed)
		{
			return EncodingHelper.GetString(DecompressBytes(EncodingHelper.GetBytes(compressed)));
		}

		public static byte[] CompressBytes(string inData)
		{
			return CompressBytes(EncodingHelper.GetBytes(inData));
		}
		public static byte[] CompressBytes(byte[] inData)
		{
			using (MemoryStream outMemoryStream = new MemoryStream())
			using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream, zlibConst.Z_DEFAULT_COMPRESSION))
			using (Stream inMemoryStream = new MemoryStream(inData))
			{
				CopyStream(inMemoryStream, outZStream);
				outZStream.finish();
				outMemoryStream.Seek(0, SeekOrigin.Begin);
				return outMemoryStream.ToArray();
			}
		}

		public static byte[] DecompressBytes(string inData)
		{
			return DecompressBytes(EncodingHelper.GetBytes(inData));
		}
		public static byte[] DecompressBytes(byte[] inData)
		{
			using (MemoryStream outMemoryStream = new MemoryStream())
			using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream))
			using (Stream inMemoryStream = new MemoryStream(inData))
			{
				CopyStream(inMemoryStream, outZStream);
				outZStream.finish();
				outMemoryStream.Seek(0, SeekOrigin.Begin);
				return outMemoryStream.ToArray();
			}
		}

		public static void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[2000];
			int len;
			while ((len = input.Read(buffer, 0, 2000)) > 0)
			{
				output.Write(buffer, 0, len);
			}
			output.Flush();
		}
	}
}

