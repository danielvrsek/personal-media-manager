namespace Services.TorrentFinder.Model
{
	public abstract class TorrentSearchResult
	{
		public abstract TorrentDetail TorrentDetail { get; set; }

		public abstract string Name { get; set; }

		public abstract string Size { get; set; }

		public abstract int Seeds { get; set; }

		public abstract int Leeches { get; set; }

		public abstract string Uploaded { get; set; }

		public abstract string RelativeDetailUrl { get; set; }
	}
}
