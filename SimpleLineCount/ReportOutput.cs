using Spectre.Console;

namespace SimpleLineCount;

/// <summary>
/// Class for printing a line counting report to the console
/// </summary>
public class ReportOutput : IReportOutput
{
	/// <summary>
	/// Writes the given report to the console
	/// </summary>
	/// <param name="report">line counting report</param>
	/// <param name="settings">settings</param>
	/// <param name="numberOfFiles">Number of included files</param>
	/// <param name="duration">duration for parsing and report generation</param>
	public void WriteReport(LineCountingReport? report, LineCountSettings settings, int numberOfFiles, TimeSpan duration)
	{
		if (report is null)
		{
			AnsiConsole.MarkupLine("[red]Error: Could not create line counting report[/]");
			return;
		}

		AnsiConsole.Clear();
		AnsiConsole.MarkupLine($"Report generation for [italic]\"{settings.Directory}\"[/] took [aqua]{duration.TotalSeconds:0.000}s[/] ([green]{numberOfFiles}[/] files parsed)");
		AnsiConsole.MarkupLine("");

		OutputLineStatistics(report.Lines);
		OutputLanguageStatistics(report.Languages);
		OutputFileStatistics(report.Files);
	}

	/// <summary>
	/// Writes line statistics
	/// </summary>
	/// <param name="lineStatistics">line statistics</param>
	private static void OutputLineStatistics(LineCountingReport.LineStatistics? lineStatistics)
	{
		if (lineStatistics is null)
		{
			return;
		}

		var linesTable = new Table();
		linesTable.AddColumn(new TableColumn("").Footer("[green bold]= Total[/]"));
		linesTable.AddColumn(new TableColumn("").RightAligned().Footer($"[green bold]{lineStatistics.TotalLines:n0}[/]"));
		linesTable.AddRow("Code", lineStatistics.CodeLines.ToString("n0"));
		linesTable.AddRow("[yellow]+[/] Comments", lineStatistics.CommentLines.ToString("n0"));
		linesTable.AddRow("[yellow]+[/] Empty", lineStatistics.EmptyLines.ToString("n0"));
		linesTable.HideHeaders();
		linesTable.ShowFooters = true;

		var linesPanel = new Panel(linesTable)
		{
			Header = new("[teal]Lines[/]"),
			Padding = new(1, 0, 1, 0)
		};

		AnsiConsole.Write(linesPanel);
	}

	/// <summary>
	/// Writes language statistics
	/// </summary>
	/// <param name="languageStatistics">language statistics</param>
	private static void OutputLanguageStatistics(LineCountingReport.LanguageStatistics? languageStatistics)
	{
		if (languageStatistics is null)
		{
			return;
		}

		var languagesTable = new Table();
		languagesTable.AddColumn(new TableColumn("[olive]Language[/]"));
		languagesTable.AddColumn(new TableColumn("[olive]Total lines[/]").RightAligned());

		foreach (var (language, totalLines) in languageStatistics.TopLanguages)
		{
			languagesTable.AddRow(language.Name!, totalLines.ToString("n0"));
		}

		var languagesPanel = new Panel(languagesTable)
		{
			Header = new("[teal]Top 5 languages[/]"),
			Padding = new(1, 0, 1, 0)
		};

		AnsiConsole.MarkupLine("");
		AnsiConsole.Write(languagesPanel);
	}

	/// <summary>
	/// Writes file statistics
	/// </summary>
	/// <param name="fileStatistics">file statistics</param>
	private static void OutputFileStatistics(LineCountingReport.FileStatistics? fileStatistics)
	{
		if (fileStatistics is null)
		{
			return;
		}

		var filesTable = new Table();
		filesTable.AddColumn(new TableColumn("[olive]File[/]"));
		filesTable.AddColumn(new TableColumn("[olive]Total lines[/]").RightAligned());

		foreach (var (filename, totalLines) in fileStatistics.TopFiles)
		{
			filesTable.AddRow(new TextPath(filename), new Markup(totalLines.ToString("n0")));
		}

		var filesPanel = new Panel(filesTable)
		{
			Header = new("[teal]Top 5 files[/]"),
			Padding = new(1, 0, 1, 0)
		};

		AnsiConsole.MarkupLine("");
		AnsiConsole.Write(filesPanel);
	}
}