namespace SimpleLineCount.FileReading
{
	public interface IFileAccess
	{
		string[] ReadAllLines(string path);

		string[] GetAllDirectoryFiles(string directory, bool recursive);

		string GetFileExtension(string filename);
	}
}