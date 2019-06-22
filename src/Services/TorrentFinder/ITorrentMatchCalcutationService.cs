using Services.TorrentFilenameParser.Model;
using Services.TorrentFinder.Model;
using System.Collections.Generic;

namespace Services.TorrentFinder
{
	public interface ITorrentMatchCalcutationService
	{
		Match<ParsedMedia, TorrentSearchResult> GetBestMatch(ParsedMedia searchedMedia, List<TorrentSearchResult> searchResults);

		IEnumerable<Match<ParsedMedia, TorrentSearchResult>> GetMatches(ParsedMedia searchedMedia, List<TorrentSearchResult> searchResults);
	}
}