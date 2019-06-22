using DelugedClient.Common;
using DelugedClient.Model;
using DelugedClient.Model.Protocol.Message;
using DelugedClient.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DelugedClient.RpcApi
{
	public class Daemon : RpcApi
	{
		public Daemon(DelugeRPCProtocol protocol) : base(protocol)
		{

		}

		public async Task<bool> LoginAsync(string username, string password)
		{
			var args = new object[] { username, password };

			IRPCMessage message = await CallMethodAsync(Method.Login, args);

			return (message as RPCResponse) != null;
		}

		public async Task<Torrent> AddTorrentMagnetAsync(string magnet)
		{
			IRPCMessage message = await CallMethodAsync(Method.AddTorrentMagnet, new object[] { magnet, new object[] { } });

			RPCResponse responseMessage = message as RPCResponse;

			if (responseMessage == null)
			{
				HandleError(message as RPCError);
			}

			string torrentId = responseMessage.ReturnValues?[0] as String;

			return torrentId != null ? new Torrent(torrentId) : null;
		}

		public async Task<object[]> GetTorrentStatusAsync(Torrent torrent, TorrentStatusKey keyFlags)
		{
			object[] args = GetTorrentStatusKeyFlags(keyFlags).Select(key => key.GetStatusKeyName()).Cast<object>().ToArray();

			IRPCMessage message = await CallMethodAsync(Method.GetTorrentStatus, new object[] { torrent.TorrentId, args });

			RPCResponse responseMessage = message as RPCResponse;

			if (responseMessage == null)
			{
				HandleError(message as RPCError);
			}

			return responseMessage.ReturnValues;
		}

		public async Task<Dictionary<object, object>> GetTorrentsStatusAsync(TorrentStatusKey keyFlags)
		{
			object[] args = GetTorrentStatusKeyFlags(keyFlags).Select(key => key.GetStatusKeyName()).Cast<object>().ToArray();

			IRPCMessage message = await CallMethodAsync(Method.GetTorrentsStatus, new object[] { new Dictionary<object, object>(), args });
			RPCResponse response = message as RPCResponse;

			if (response == null)
			{
				HandleError(message as RPCError);
			}

			return response.ReturnValues.Cast<Dictionary<object, object>>().Single();
		}

		private IEnumerable<TorrentStatusKey> GetTorrentStatusKeyFlags(TorrentStatusKey keyFlags)
		{
			Type type = typeof(TorrentStatusKey);
			var values = Enum.GetValues(type).Cast<TorrentStatusKey>();

			foreach (TorrentStatusKey value in values)
			{
				if (keyFlags.HasFlag(value))
				{
					yield return value;
				}
			}
		}

		private void HandleError(RPCError errorMessage)
		{
			string exceptionMessage = errorMessage != null ? errorMessage.ExceptionMessage : "Unknown error occured.";
			throw new Exception(exceptionMessage);
		}
	}
}
