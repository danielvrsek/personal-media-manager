using Services.TorrentFilenameParser.Model;

namespace Services.TorrentFilenameParser
{
	public interface ITorrentFilenameParserService
	{
		ParsedMedia Parse(string filename);
	}
}