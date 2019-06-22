using System;

namespace DelugedClient.Model.Protocol.Message
{
	public class RPCResponse : IRPCMessage
	{
		public MessageType MessageType => MessageType.Response;

		public object[] ReturnValues { get; private set; }

		static public explicit operator RPCResponse(object[] data)
		{
			if ((MessageType)data[0] != MessageType.Response)
			{
				throw new Exception("Response is not a ResponseMessage.");
			}

			var returnValues = data[2] as Object[];

			if (returnValues == null && data[2] != null)
			{
				returnValues = new[] { data[2] };
			}

			return new RPCResponse()
			{
				ReturnValues = returnValues
			};
		}
	}
}
