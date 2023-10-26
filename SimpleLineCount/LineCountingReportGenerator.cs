namespace SimpleLineCount;

/// <summary>
/// Class for creating a line counting report
/// </summary>
public class LineCountingReportGenerator : ILineCountingReportGenerator
{
	/// <summary>
	/// Creates a report for the given list of source files
	/// </summary>
	/// <param name="sourceFiles">source files</param>
	/// <returns>line counting report</returns>
	public LineCountingReport CreateReport(List<SourceFile> sourceFiles)
	{
		int codeLines = sourceFiles.Sum(x => x.Statistics.CodeLines);
		int commentLines = sourceFiles.Sum(x => x.Statistics.CommentLines);
		int emptyLines = sourceFiles.Sum(x => x.Statistics.EmptyLines);
		int totalLines = codeLines + commentLines + emptyLines;

		LineCountingReport report = new()
		{
			Lines = new(totalLines, codeLines, commentLines, emptyLines)
		};

		return report;
	}
}