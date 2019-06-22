using Services.LocalMediaDiscovery.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Services.LocalMediaDiscovery
{
	public class LocalMediaDiscoveryService
	{
		private readonly LocalMediaDiscoverySettings _settings;
		private readonly string _rootPath;

		public LocalMediaDiscoveryService(string path, LocalMediaDiscoverySettings settings) : this(path)
		{
			_settings = settings;
		}

		public LocalMediaDiscoveryService(string path)
		{
			_rootPath = path;
			_settings = new LocalMediaDiscoverySettings();
		}

		public List<LocalMediaFile> GetFiles()
		{
			return GetFiles(_rootPath, _settings);
		}

		public static List<LocalMediaFile> GetFiles(string path, LocalMediaDiscoverySettings settings)
		{
			List<LocalMediaFile> mediaFiles = new List<LocalMediaFile>();

			if (!Directory.Exists(path))
			{
				throw new DirectoryNotFoundException($"Directory {path} does not exist.");
			}

			foreach (string fileName in Directory.GetFiles(path))
			{
				if (IsMediaFile(fileName, settings))
				{
					mediaFiles.Add(new LocalMediaFile(fileName));
				}
			}

			if (settings.Recursive)
			{
				foreach (string folderPath in Directory.GetDirectories(path))
				{
					mediaFiles.AddRange(GetFiles(folderPath, settings));
				}
			}

			return mediaFiles;
		}

		public static bool IsMediaFile(string fileName, LocalMediaDiscoverySettings settings)
		{
			string extension = Path.GetExtension(fileName).ToLower();

			if (extension == null)
			{
				return false;
			}

			if (!settings.Extensions.Contains(extension))
			{
				return false;
			}

			if (settings.ExcludeByPattern != null)
			{
				if (Regex.IsMatch(fileName, settings.ExcludeByPattern))
				{
					return false;
				}
			}
		
			return true;
		}
	}
}
