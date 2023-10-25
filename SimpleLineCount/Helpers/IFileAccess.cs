namespace SimpleLineCount.Helpers;

/// <summary>
/// Interface for abstracting access to <see cref="File"/> and <see cref="Directory"/>
/// </summary>
internal interface IFileAccess
{
	/// <summary>
	/// Returns an enumerable collection of full file names in the directory specified by <paramref name="path"/>
	/// </summary>
	/// <param name="path">directory</param>
	/// <returns>enumerable collection of full file names</returns>
	IEnumerable<string> DirectoryEnumerateFiles(string path);

	/// <summary>
	/// Opens a text file, reads all lines of the file, and then closes the file
	/// </summary>
	/// <param name="path">file path</param>
	/// <returns>text lines</returns>
	Task<string[]> ReadAllLinesAsync(string path);
}