using DelugedClient.Common;
using DelugedClient.Model.Protocol.Message;
using rencodesharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DelugedClient.Protocol
{
	public class DelugeRPCProtocol : Protocol
	{
		private int _requestId = 0;
		private Dictionary<int, Action<IRPCMessage>> _messageCallbacks;

		public DelugeRPCProtocol(Transport transport) : base(transport)
		{
			_messageCallbacks = new Dictionary<int, Action<IRPCMessage>>();
		}

		protected override void OnDataReceived(string data)
		{
			base.OnDataReceived(data);

			var decompressedData = CompressionHelper.Decompress(data);
			var decodedData = Rencode.Decode(decompressedData) as Object[];

			IRPCMessage message = DecodeMessage(decodedData);

			switch (message.MessageType)
			{
				case MessageType.Response:
				case MessageType.Error:
					int requestId = (int)decodedData[1];
					if (_messageCallbacks.TryGetValue(requestId, out Action<IRPCMessage> callback))
					{
						callback(message);
						_messageCallbacks.Remove(requestId);
					}
					break;
				case MessageType.Event:
					Console.WriteLine("Event");
					break;
				default:
					throw new NotSupportedException("Specified MessageType is not supported.");
			}
		}

		private IRPCMessage DecodeMessage(object[] data)
		{
			IRPCMessage message;

			MessageType messageType = (MessageType)data[0];

			switch (messageType)
			{
				case MessageType.Response:
					message = (RPCResponse)data;
					break;
				case MessageType.Error:
					message = (RPCError)data;
					break;
				case MessageType.Event:
					message = (RPCEvent)data;
					break;
				default:
					throw new Exception("Response from Deluge Daemon is in invalid format. Code: 2");
			}

			return message;
		}

		public void Call(string method, object[] args, Dictionary<object, object> kwargs, Action<IRPCMessage> callback)
		{
			_messageCallbacks.Add(_requestId, callback);

			object data = new object[] { new object[] { _requestId, method, args, kwargs } };

			SendData(CompressionHelper.Compress(Rencode.Encode(data)));
			_requestId++;
		}

		public async Task<IRPCMessage> CallAsync(string method, object[] args, Dictionary<object, object> kwargs)
		{
			TaskCompletionSource<IRPCMessage> taskCompletionSource = new TaskCompletionSource<IRPCMessage>();
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(500);
			cancellationTokenSource.Token.Register(() => taskCompletionSource.TrySetResult(null));

			Call(method, args, kwargs, (m) => taskCompletionSource.TrySetResult(m));

			return await taskCompletionSource.Task;
		}

		public override void SendData(string data)
		{
			Transport.Write(data);
		}
	}
}
