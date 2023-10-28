using SimpleLineCount.Config;
using SimpleLineCount.Helpers;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics;

namespace SimpleLineCount;

/// <summary>
/// Command implementation for line counting
/// </summary>
internal class LineCountCommand : AsyncCommand<LineCountSettings>
{
	/// <inheritdoc />
	public override async Task<int> ExecuteAsync(CommandContext context, LineCountSettings settings)
	{
		if (!Directory.Exists(settings.Directory))
		{
			AnsiConsole.MarkupLine("[red]Error: Invalid directory[/]");
			return 1;
		}

		LineCountingReport? report = null;
		int numberOfFiles = 0;
		Stopwatch sw = Stopwatch.StartNew();

		await AnsiConsole
			.Status()
			.Spinner(Spinner.Known.Star)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Parsing files", async ctx =>
			{
				IFileAccess fileAccess = new Helpers.FileAccess();
				IFileReader fileReader = new FileReader(fileAccess, new LineCounting(), new ConfigReader(fileAccess));
				var files = (await fileReader.ReadFilesAsync(settings)).ToList();
				numberOfFiles = files.Count;
				AnsiConsole.MarkupLine($"[green]Successfully parsed {files.Count} files[/]");

				ctx.Status("Creating report");

				ILineCountingReportGenerator reportGenerator = new LineCountingReportGenerator();
				report = reportGenerator.CreateReport(files);
			});

		sw.Stop();
		TimeSpan duration = sw.Elapsed;

		OutputReport(report, settings, numberOfFiles, duration);

		return 0;
	}

	/// <summary>
	/// Writes the given report to the console
	/// </summary>
	/// <param name="report">line counting report</param>
	/// <param name="settings">settings</param>
	/// <param name="numberOfFiles">Number of included files</param>
	/// <param name="duration">duration for parsing and report generation</param>
	private static void OutputReport(LineCountingReport? report, LineCountSettings settings, int numberOfFiles, TimeSpan duration)
	{
		if (report is null)
		{
			AnsiConsole.MarkupLine("[red]Error: Could not create line counting report[/]");
			return;
		}

		AnsiConsole.Clear();
		AnsiConsole.MarkupLine($"Report generation for [italic]\"{settings.Directory}\"[/] took [aqua]{duration.TotalSeconds:0.000}s[/] ([green]{numberOfFiles}[/] files included)");
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
		linesTable.AddRow("[olive]+[/] Comments", lineStatistics.CommentLines.ToString("n0"));
		linesTable.AddRow("[olive]+[/] Empty", lineStatistics.EmptyLines.ToString("n0"));
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

		var languagesTable = new Table()
		{
			ShowHeaders = false
		};
		languagesTable.AddColumn(new TableColumn(""));
		languagesTable.AddColumn(new TableColumn("").RightAligned());

		foreach (var (language, totalLines) in languageStatistics.TopLanguages)
		{
			languagesTable.AddRow(language.Name!, totalLines.ToString("n0"));
		}

		var languagesPanel = new Panel(languagesTable)
		{
			Header = new("[teal]Top languages[/]"),
			Padding = new(1, 0, 1, 0)
		};

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

		var filesTable = new Table()
		{
			ShowHeaders = false
		};
		filesTable.AddColumn(new TableColumn(""));
		filesTable.AddColumn(new TableColumn("").RightAligned());

		foreach (var (filename, totalLines) in fileStatistics.TopFiles)
		{
			filesTable.AddRow(new TextPath(filename), new Markup(totalLines.ToString("n0")));
		}

		var filesPanel = new Panel(filesTable)
		{
			Header = new("[teal]Top files[/]"),
			Padding = new(1, 0, 1, 0)
		};

		AnsiConsole.Write(filesPanel);
	}
}