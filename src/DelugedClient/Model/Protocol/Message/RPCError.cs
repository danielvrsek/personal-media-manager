using System;

namespace DelugedClient.Model.Protocol.Message
{
	public class RPCError : IRPCMessage
	{
		public MessageType MessageType => MessageType.Error;

		public string ExceptionType { get; private set; }

		public string ExceptionMessage { get; private set; }

		public string Traceback { get; private set; }

		static public explicit operator RPCError(object[] data)
		{
			if ((MessageType)data[0] != MessageType.Error)
			{
				throw new Exception("Response is not an Error.");
			}

			var exceptionData = data[2] as Object[];

			if (exceptionData == null)
			{
				throw new Exception("Response is not a valid ErrorMessage. Some of the parameters don't match.");
			}

			var exceptionType = exceptionData[0] as String;
			var exceptionMessage = exceptionData[1] as String;
			var traceback = exceptionData[2] as String;

			if (exceptionType == null ||
				exceptionMessage == null ||
				traceback == null)
			{
				throw new Exception("Response is not a valid ErrorMessage. Some of the parameters don't match.");
			} 

			return new RPCError()
			{
				ExceptionType = exceptionType,
				ExceptionMessage = exceptionMessage,
				Traceback = traceback
			};
		}

	}
}
