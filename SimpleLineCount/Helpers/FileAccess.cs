namespace SimpleLineCount.Helpers;

/// <summary>
/// Class for abstracting access to <see cref="File"/> and <see cref="Directory"/>
/// </summary>
internal class FileAccess : IFileAccess
{
	/// <summary>
	/// Returns an enumerable collection of full file names in the directory specified by <paramref name="path"/>
	/// </summary>
	/// <param name="path">directory</param>
	/// <returns>enumerable collection of full file names</returns>
	public IEnumerable<string> DirectoryEnumerateFiles(string path)
	{
		return Directory.EnumerateFiles(path);
	}

	/// <summary>
	/// Opens a text file, reads all lines of the file, and then closes the file
	/// </summary>
	/// <param name="path">file path</param>
	/// <returns>text lines</returns>
	public Task<string[]> ReadAllLinesAsync(string path)
	{
		return File.ReadAllLinesAsync(path);
	}
}