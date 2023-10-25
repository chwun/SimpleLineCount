namespace SimpleLineCount;

/// <summary>
/// Interface for line counting
/// </summary>
internal interface ILineCounting
{
	/// <summary>
	/// Counts lines for the given file content
	/// </summary>
	/// <param name="sourceFile">source file</param>
	/// <param name="fileContent">file content as lines</param>
	void CountLines(SourceFile sourceFile, string[] fileContent);
}