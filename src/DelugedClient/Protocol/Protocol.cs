using System;
using System.Collections.Generic;
using System.Text;

namespace DelugedClient.Protocol
{
	public abstract class Protocol
	{
		public Transport Transport { get; }

		public Protocol(Transport transport)
		{
			this.Transport = transport;
			this.OnConnectionMade();
			this.Transport.Read();
			Transport.DataReceived += OnDataReceived;
		}

		private void OnDataReceived(object sender, EventArgs e)
		{
			DataReceivedEventArgs eventArgs = e as DataReceivedEventArgs;

			OnDataReceived(eventArgs?.Data);
		}

		protected virtual void OnDataReceived(string data)
		{

		}

		protected virtual void OnConnectionMade()
		{
			
		}

		protected virtual void OnConnectionLost()
		{

		}

		public abstract void SendData(string data);
	}
}

