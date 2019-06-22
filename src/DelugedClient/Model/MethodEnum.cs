using DelugedClient.Common;

namespace DelugedClient.Model
{
	public enum Method
	{
		[MethodName("daemon.login")]
		Login,

		[MethodName("daemon.authorized_call")]
		AuthorizedCall,

		[MethodName("daemon.get_method_list")]
		GetMethodList,

		[MethodName("daemon.shutdown")]
		Shutdown,

		[MethodName("core.add_torrent_file")]
		AddTorrentFile,

		[MethodName("core.add_torrent_magnet")]
		AddTorrentMagnet,

		[MethodName("core.add_torrent_url")]
		AddTorrentUrl,

		[MethodName("core.create_account")]
		CreateAccount,

		[MethodName("core.get_config")]
		GetConfig,

		[MethodName("core.get_config_value")]
		GetConfigValue,

		[MethodName("core.get_external_ip")]
		GetExternalIP,

		[MethodName("core.get_free_space")]
		GetFreeSpace,

		[MethodName("core.get_listen_port")]
		GetListenPort,

		[MethodName("core.get_session_state")]
		GetSessionState,

		[MethodName("core.remove_torrent")]
		RemoveTorrent,

		[MethodName("core.remove_torrents")]
		RemoveTorrents,

		[MethodName("core.get_torrent_status")]
		GetTorrentStatus,

		[MethodName("core.get_torrents_status")]
		GetTorrentsStatus,
	}
}
