namespace SimpleLineCount.Models;

/// <summary>
/// Data for a source file language
/// </summary>
public class SourceFileLanguage
{
	/// <summary>
	/// Language name (should be unique)
	/// Example: "C#"
	/// </summary>
	public string Name { get; init; } = "";

	/// <summary>
	/// List of file extensions (must be unique)
	/// Example: "cs", "csx"
	/// </summary>
	public List<string> FileExtensions { get; init; } = [];

	/// <summary>
	/// Single line comment token
	/// Example: "//"
	/// </summary>
	public string SingleLineCommentToken { get; init; } = "";

	/// <summary>
	/// Multi line comment token
	/// Example: ("/*", "*/")
	/// </summary>
	public (string StartToken, string EndToken)? MultiLineCommentTokens { get; init; }
}