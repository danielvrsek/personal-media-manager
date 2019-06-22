using System;
using System.Collections.Generic;

namespace DelugedClient.Model.Protocol.Message
{
	public class RPCEvent : IRPCMessage
	{
		public MessageType MessageType => MessageType.Event;

		public string EventName { get; private set; }

		public IEnumerable<object> Data { get; private set; }

		static public explicit operator RPCEvent(object[] data)
		{
			if ((MessageType)data[0] != MessageType.Event)
			{
				throw new Exception("Response is not an Event.");
			}

			var eventName = data[1] as String;
			var eventData = data[2] as Object[];

			if (eventName == null ||
				eventData == null)
			{
				throw new Exception("Response is not a valid EventMessage. Some of the parameters don't match.");
			}

			return new RPCEvent()
			{
				EventName = eventName,
				Data = eventData
			};
		}
	}
}
