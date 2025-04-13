using SimpleLineCount.Commands.LineCount;
using SimpleLineCount.Models;

namespace SimpleLineCount.Services;

/// <summary>
/// Interface for printing a line counting report to the console
/// </summary>
public interface IReportOutputWriter
{
	/// <summary>
	/// Writes the given report to the console
	/// </summary>
	/// <param name="report">line counting report</param>
	/// <param name="settings">settings</param>
	/// <param name="numberOfFiles">Number of included files</param>
	/// <param name="duration">duration for parsing and report generation</param>
	void WriteReport(LineCountingReport? report, LineCountSettings settings, int numberOfFiles, TimeSpan duration);
}