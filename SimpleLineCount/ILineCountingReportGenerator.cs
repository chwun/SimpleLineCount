namespace SimpleLineCount;

/// <summary>
/// Interface for creating a line counting report
/// </summary>
public interface ILineCountingReportGenerator
{
	/// <summary>
	/// Creates a report for the given list of source files
	/// </summary>
	/// <param name="sourceFiles">source files</param>
	/// <returns>line counting report</returns>
	LineCountingReport CreateReport(List<SourceFile> sourceFiles);
}