using System.IO;

namespace SimpleLineCount.FileReading
{
	public class FileAccess : IFileAccess
	{
		public string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		public string[] GetAllDirectoryFiles(string directory, bool recursive)
		{
			return Directory.GetFiles(directory, "*.*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
		}

		public string GetFileExtension(string filename)
		{
			return Path.GetExtension(filename);
		}
	}
}