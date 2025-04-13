namespace SimpleLineCount.Helpers;

/// <summary>
/// Class for abstracting access to <see cref="File"/> and <see cref="Directory"/>
/// </summary>
public class FileAccess : IFileAccess
{
	/// <inheritdoc/>
	public IEnumerable<string> DirectoryEnumerateFiles(string path)
	{
		return Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories);
	}

	/// <inheritdoc/>
	public DirectoryInfo? DirectoryGetParent(string path)
	{
		return Directory.GetParent(path);
	}

	/// <inheritdoc/>
	public Task<string[]> ReadAllLinesAsync(string path)
	{
		return File.ReadAllLinesAsync(path);
	}

	/// <inheritdoc/>
	public Task<string> ReadAllTextAsync(string path)
	{
		return File.ReadAllTextAsync(path);
	}
}