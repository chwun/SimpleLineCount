namespace SimpleLineCount;

/// <summary>
/// Class for creating a line counting report
/// </summary>
public class LineCountingReportGenerator : ILineCountingReportGenerator
{
	/// <inheritdoc/>
	public LineCountingReport CreateReport(List<SourceFile> sourceFiles)
	{
		int codeLines = sourceFiles.Sum(x => x.Statistics.CodeLines);
		int commentLines = sourceFiles.Sum(x => x.Statistics.CommentLines);
		int emptyLines = sourceFiles.Sum(x => x.Statistics.EmptyLines);
		int totalLines = sourceFiles.Sum(x => x.Statistics.TotalLines);

		var topLanguages = new List<(SourceFileLanguage Language, int TotalLines)>(
			sourceFiles
				.GroupBy(x => x.Language!)
				.Select(x => (x.Key, x.Sum(y => y.Statistics.TotalLines)))
				.OrderByDescending(x => x.Item2)
				.Take(5));

		var topFiles = new List<(string Filename, int TotalLines)>(
			sourceFiles
				.OrderByDescending(x => x.Statistics.TotalLines)
				.Take(5)
				.Select(x => (x.FileName, x.Statistics.TotalLines)));

		LineCountingReport report = new()
		{
			Lines = new(totalLines, codeLines, commentLines, emptyLines),
			Languages = new(topLanguages),
			Files = new(topFiles)
		};

		return report;
	}
}