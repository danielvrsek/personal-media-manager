using DelugedClient.Common;
using DelugedClient.Model;
using DelugedClient.Model.Protocol.Message;
using DelugedClient.Protocol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DelugedClient.RpcApi
{
	public abstract class RpcApi
    {
		protected DelugeRPCProtocol Protocol { get; set; }

		protected RpcApi(DelugeRPCProtocol protocol)
		{
			Protocol = protocol;
		}

		public void CallMethod(Method method, Action<IRPCMessage> callback)
		{
			CallMethod(method, new object[] { }, callback);
		}
		public void CallMethod(Method method, object[] args, Action<IRPCMessage> callback)
		{
			CallMethod(method, args, new Dictionary<object, object>(), callback);
		}

		public void CallMethod(Method method, object[] args, Dictionary<object, object> kwargs, Action<IRPCMessage> callback)
		{
			Protocol.Call(method.GetMethodName(), args, kwargs, callback);
		}

		public async Task<IRPCMessage> CallMethodAsync(Method method)
		{
			return await CallMethodAsync(method, new object[] { });
		}

		public async Task<IRPCMessage> CallMethodAsync(Method method, object[] args)
		{
			return await CallMethodAsync(method, args, new Dictionary<object, object>());
		}

		public async Task<IRPCMessage> CallMethodAsync(Method method, object[] args, Dictionary<object, object> kwargs)
		{
			return await Protocol.CallAsync(method.GetMethodName(), args, kwargs);
		}
	}
}
