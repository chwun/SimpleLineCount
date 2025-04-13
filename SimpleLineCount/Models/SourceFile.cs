namespace SimpleLineCount.Models;

/// <summary>
/// Data for one single source file
/// </summary>
public class SourceFile
{
	/// <summary>
	/// File name
	/// </summary>
	public string FileName { get; init; } = "";

	/// <summary>
	/// Language of this file
	/// </summary>
	public SourceFileLanguage? Language { get; init; }

	/// <summary>
	/// File statistics
	/// </summary>
	public LineCountStatistics Statistics { get; set; } = new();
}