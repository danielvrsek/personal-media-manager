using DelugedClient.Protocol;
using DelugedClient.RpcApi;
using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace DelugedClient.DelugeConnection
{
	public class DelugedConnectionService : IDelugedConnectionService
	{
		private static DelugedConnectionService _connection;

		public static DelugedConnectionService GetConnection(string hostname = "127.0.0.1", int port = 58846)
		{
			if (_connection != null)
			{
				if (hostname != _connection._hostname || port != _connection._port)
				{
					_connection.Close();

					return (_connection = new DelugedConnectionService(hostname, port));
				}
			}

			return _connection ?? (_connection = new DelugedConnectionService(hostname, port));
		}

		public Daemon Daemon { get; }

		private string _hostname;
		private int _port;

		private DelugedConnectionService(string hostname, int port)
		{
			DelugeRPCProtocol protocol = CreateConnection(hostname, port);

			Daemon = new Daemon(protocol);

			_hostname = hostname;
			_port = port;
		}

		private DelugeRPCProtocol CreateConnection(string hostname, int port)
		{
			TcpClient client = new TcpClient(hostname, port);
			SslStream sslStream = new SslStream(client.GetStream(), false,
				new RemoteCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true));
			//sslStream.AuthenticateAsClient("DelugeClient", null, false);
			sslStream.AuthenticateAsClient(hostname, new X509Certificate2Collection(), SslProtocols.Tls12, false);
			Transport transport = new Transport(sslStream);

			return new DelugeRPCProtocol(transport);
		}

		public void Close()
		{
			throw new NotImplementedException();
		}
    }
}