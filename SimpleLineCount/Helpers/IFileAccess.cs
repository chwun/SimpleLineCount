namespace SimpleLineCount.Helpers;

/// <summary>
/// Interface for abstracting access to <see cref="File"/> and <see cref="Directory"/>
/// </summary>
public interface IFileAccess
{
	/// <summary>
	/// Determines whether the given path refers to an existing directory on disk
	/// </summary>
	/// <param name="path">the path to test</param>
	/// <returns>true if path refers to an existing directory; false if the directory does not exist or an error occurs while trying to determine if the directory exists</returns>
	bool DirectoryExists(string path);

	/// <summary>
	/// Returns an enumerable collection of full file names in the directory specified by <paramref name="path"/>
	/// </summary>
	/// <param name="path">directory</param>
	/// <returns>enumerable collection of full file names</returns>
	IEnumerable<string> DirectoryEnumerateFiles(string path);

	/// <summary>
	/// Retrieves the parent directory of the specified path, including both absolute and relative paths
	/// </summary>
	/// <param name="path">the path for which to retrieve the parent directory</param>
	/// <returns>the parent directory, or null if path is the root directory, including the root of a UNC server or share name</returns>
	DirectoryInfo? DirectoryGetParent(string path);

	/// <summary>
	/// Opens a text file, reads all lines of the file, and then closes the file
	/// </summary>
	/// <param name="path">file path</param>
	/// <returns>text lines</returns>
	Task<string[]> ReadAllLinesAsync(string path);

	/// <summary>
	/// Opens a text file, reads all text, and then closes the file
	/// </summary>
	/// <param name="path">file path</param>
	/// <returns>string containing all text in the file</returns>
	Task<string> ReadAllTextAsync(string path);
}