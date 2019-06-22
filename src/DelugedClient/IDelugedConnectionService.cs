using DelugedClient.RpcApi;

namespace DelugedClient.DelugeConnection
{
	public interface IDelugedConnectionService
	{
		Daemon Daemon { get; }

		void Close();
	}
}