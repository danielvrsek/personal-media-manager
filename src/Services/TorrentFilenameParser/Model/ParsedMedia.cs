namespace Services.TorrentFilenameParser.Model
{
	public class ParsedMedia
    {
		public string Title { get; set; }
		public string Year { get; set; }
		public string Season { get; set; }
		public string Episode { get; set; }
		public string Quality { get; set; }
		public string Resolution { get; set; }
		public string Codec { get; set; }
		public string Audio { get; set; }
		public string Group { get; set; }
		public string Size { get; set; }

		public override string ToString()
		{
			return $"Title: {Title ?? "null"}\nYear: {Year ?? "null"}\nSeason: {Season ?? "null"}\nEpisode: {Episode ?? "null"}\nQuality: {Quality ?? "null"}\n" +
				$"Resolution: {Resolution ?? "null"}\nCodec: {Codec ?? "null"}\nAudio: {Audio ?? "null"}\nGroup: {Group ?? "null"}\nSize: {Size ?? "null"}";
		}
	}
}
