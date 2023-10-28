namespace SimpleLineCount;

/// <summary>
/// Report with line counting data
/// </summary>
public class LineCountingReport
{
	/// <summary>
	/// Record for line statistics
	/// </summary>
	/// <param name="TotalLines">total number of lines</param>
	/// <param name="CodeLines">number of code lines</param>
	/// <param name="CommentLines">number of comment lines</param>
	/// <param name="EmptyLines">number of empty lines</param>
	public record LineStatistics(int TotalLines, int CodeLines, int CommentLines, int EmptyLines);

	/// <summary>
	/// Record for language statistics
	/// </summary>
	/// <param name="TopLanguages">languages with the most lines</param>
	public record LanguageStatistics(List<(SourceFileLanguage Language, int TotalLines)> TopLanguages);

	/// <summary>
	/// Line statistics
	/// </summary>
	public LineStatistics? Lines { get; init; }

	/// <summary>
	/// Language statistics
	/// </summary>
	/// <value></value>
	public LanguageStatistics? Languages { get; init; }
}