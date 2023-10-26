namespace SimpleLineCount;

/// <summary>
/// Interface for line counting
/// </summary>
public interface ILineCounting
{
	/// <summary>
	/// Counts lines for the given file content and applies the result to the given source file
	/// </summary>
	/// <param name="sourceFile">source file</param>
	/// <param name="fileContent">file content as lines</param>
	void CountLines(SourceFile sourceFile, string[] fileContent);
}