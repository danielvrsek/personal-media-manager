using DelugedClient.Common;
using DelugedClient.Model.Protocol;
using System;
using System.IO;

namespace DelugedClient.Protocol
{
	public delegate void DataReceivedEventHandler(object sender, EventArgs e);

	public class Transport
	{
		public const int READ_BUFFER_SIZE = 1024;

		public event DataReceivedEventHandler DataReceived;

		public Stream Stream { get; set; }

		public Transport(Stream stream)
		{
			Stream = stream;
		}

		public void Read()
		{
			byte[] buffer = new byte[READ_BUFFER_SIZE];
			Read(new ReadInfo(buffer, this.Stream));
		}

		public void Read(ReadInfo info)
		{
			info.Stream.BeginRead(info.Buffer, 0, info.Buffer.Length,
							 new AsyncCallback(ReadCallback), info);
		}

		private void ReadCallback(IAsyncResult ar)
		{
			ReadInfo info = ar.AsyncState as ReadInfo;

			int read = 0;
			try
			{
				read = info.Stream.EndRead(ar);
			}
			catch (IOException ex)
			{
				throw new Exception("Read Error", ex);
			}

			string data = EncodingHelper.GetString(info.Buffer, 0, read);

			DataReceived(this, new DataReceivedEventArgs() { Data = data });

			Read(info);
		}

		public void Write(string data)
		{
			Write(EncodingHelper.GetBytes(data));
		}

		public void Write(byte[] data)
		{
			Write(data, 0, data.Length);
		}

		public void Write(byte[] data, int offset, int count)
		{
			Stream.Write(data, offset, count);
		}
	}

	internal class DataReceivedEventArgs : EventArgs
	{
		public string Data { get; set; }
	}
}
