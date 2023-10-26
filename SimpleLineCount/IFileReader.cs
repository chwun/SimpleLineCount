namespace SimpleLineCount;

/// <summary>
/// Interface for reading all relevant source files
/// </summary>
public interface IFileReader
{
	/// <summary>
	/// Parses the files specified by the given settings object into an enumerable of source files
	/// </summary>
	/// <param name="settings">settings</param>
	/// <returns>list of source files</returns>
	Task<List<SourceFile>> ReadFilesAsync(LineCountSettings settings);
}