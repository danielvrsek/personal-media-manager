using DelugedClient.DelugeConnection;
using DelugedClient.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.Attributes;

namespace Tests.TestClasses
{
	[TestClass]
    public class DelugedClientTests
    {
		private readonly IDelugedConnectionService _delugedConnection;

		public DelugedClientTests(IDelugedConnectionService delugedConnection)
		{
			this._delugedConnection = delugedConnection;
		}

		[TestMethod]
		public async void DelugeTestAsync()
		{
			Console.WriteLine(await _delugedConnection.Daemon.LoginAsync("daniel", "Eldorado12") ? "Login successful" : "Login failed");
			//string magnet = WebUtility.UrlDecode("magnet:?xt=urn:btih:D192A70CA3BC89F2E4454EFA48B4D14130C9ABE0&dn=The+Dark+Tower+2017+720p+HD+x264+AAC&tr=udp://tracker.openbittorrent.com:80/announce&tr=udp://tracker.opentrackr.org:1337/announceUDP://TRACKER.LEECHERS-PARADISE.ORG:6969/ANNOUNCE&tr=udp://tracker.zer0day.to:1337/announce&tr=udp://tracker.leechers-paradise.org:6969/announce&tr=udp://coppersurfer.tk:6969/announce");
			string magnet = WebUtility.UrlDecode("magnet:?xt=urn:btih:B5719B6A5D744E0DCC3F8879F30F52F7B84C14A2&dn=Spider-Man: Homecoming (2017) 1080p AMZN WEB-DL 6CH 3.2GB - MkvCage&tr=udp://tracker.coppersurfer.tk:6969/announce&tr=udp://tracker.ilibr.org:80/announce&tr=udp://tracker.ilibr.org:6969/announce&tr=udp://tracker.coppersurfer.tk:6969&tr=udp://tracker.opentrackr.org:1337/announce&tr=udp://tracker.zer0day.to:1337/announce&tr=udp://p4p.arenabg.com:1337/announce&tr=udp://p4p.arenabg.ch:1337/announce&tr=udp://ipv4.tracker.harry.lu:80/announce&tr=udp://explodie.org:6969/announce&tr=udp://9.rarbg.to:2710/announce&tr=udp://9.rarbg.com:2710/announce&tr=udp://9.rarbg.me:2710/announce&tr=udp://eddie4.nl:6969/announce&tr=udp://tracker.zer0day.to:1337/announce&tr=udp://tracker.leechers-paradise.org:6969/announce&tr=udp://coppersurfer.tk:6969/announce");
			//string magnet = WebUtility.UrlDecode("magnet:?xt=urn:btih:8BC46562E4FB65BC60F220C841A31C465C725615&dn=True.Memoirs.of.an.International.Assassin.2016.HDRip.XviD.AC3-EVO&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.ilibr.org%3A80%2Fannounce&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969%2Fannounce&tr=udp%3A%2F%2Feddie4.nl%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.ilibr.org%3A6969%2Fannounce&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.zer0day.to%3A1337%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2710%2Fannounce&tr=udp%3A%2F%2F9.rarbg.to%3A2710%2Fannounce&tr=udp%3A%2F%2F9.rarbg.com%3A2710%2Fannounce&tr=udp%3A%2F%2Fexplodie.org%3A6969%2Fannounce&tr=udp%3A%2F%2Fipv4.tracker.harry.lu%3A80%2Fannounce&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337%2Fannounce&tr=udp%3A%2F%2Fp4p.arenabg.ch%3A1337%2Fannounce&tr=udp%3A%2F%2Ftracker.zer0day.to%3A1337%2Fannounce&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969%2Fannounce&tr=udp%3A%2F%2Fcoppersurfer.tk%3A6969%2Fannounce");

			Torrent t = await _delugedConnection.Daemon.AddTorrentMagnetAsync(magnet);

			if (t == null)
			{
				Console.WriteLine("Torrent has been already added.");
				return;
			}

			while (true)
			{
				Dictionary<object, object> dict = (await _delugedConnection.Daemon.GetTorrentStatusAsync(t, TorrentStatusKey.State))[0] as Dictionary<object, object>;

				foreach (KeyValuePair<object, object> kvp in dict)
				{
					Console.WriteLine($"{kvp.Key} : {kvp.Value}");
				}

				Console.WriteLine("******************************************");

				await Task.Delay(10000);
			}
		}
	}
}
