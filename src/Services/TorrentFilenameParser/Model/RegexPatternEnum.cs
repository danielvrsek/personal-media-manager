using Services.TorrentFilenameParser.Common;

namespace Services.TorrentFilenameParser.Model
{
	internal enum RegexPattern
	{
		[Pattern(@"(?:[Ss]|[Ss]eason )([0-9]{1,2})(?:(?i:[ex])|\W)")]
		Season,
		[Pattern(@"(?:(?i:[ex])|[Ee]pisode )([0-9]{1,2})\W")]
		Episode,
		[Pattern(@"\W((?:19[0-9]|20[0-2])[0-9])\W")]
		Year,
		[Pattern(@"([0-9]{3,4}p)")]
		Resolution,
		[Pattern(@"\W((?i:HDRip|DVDRip|DVDScr|CamRip|Cam|(?:HD)?CAM|W[EB]BRip|BluRay|telesync|B[DR]Rip(?:PPV\.)?|[HP]DTV|(?:HD-?)?TS|(?:PPV )?WEB[\.-]?(?:DL)?(?: DVDRip)?))\W")]
		Quality,
		[Pattern(@"\W(?i:(xvid|[hx]\.?26[45]))\W")]
		Codec,
		[Pattern(@"\W((?i:MP3|DD5\.?1|Dual[\- ]Audio|LiNE|DTS|AAC[.-]LC|AAC(?:\.?2\.0)?|AC3(?:\.5\.1)?))\W")]
		Audio,
		[Pattern(@"(- ?([^-]+(?:-={[^-]+-?$)?))$")] // (?:(?:[-\.[])([^\.\- \n]+))(?(1)\.[^\.\n]+)\n
		Group,
		[Pattern(@"(\d+(?:\.\d+)?(?:GB|MB))")]
		Size
	}
}
