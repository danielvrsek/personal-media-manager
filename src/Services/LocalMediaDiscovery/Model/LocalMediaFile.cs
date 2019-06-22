using System;

namespace Services.LocalMediaDiscovery.Model
{
	public class LocalMediaFile
	{
		public string FullPath { get; }

		public string Path => FullPath.Remove(FullPath.LastIndexOf('\\') + 1);

		public string FileName => System.IO.Path.GetFileName(FullPath);

		public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(FullPath);

		public string Extension => GetExtension(FileName);

		public LocalMediaFile(string fullPath)
		{
			FullPath = fullPath;
		}

		public static string GetExtension(string fileName)
		{
			string extension = System.IO.Path.GetExtension(fileName);

			return !String.IsNullOrWhiteSpace(extension) ? extension : null;
		}
	}
}
