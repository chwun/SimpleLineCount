using Spectre.Console;
using Spectre.Console.Cli;

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

		await AnsiConsole
			.Status()
			.Spinner(Spinner.Known.Star)
			.SpinnerStyle(Style.Parse("green bold"))
			.StartAsync("Parsing files", async ctx =>
			{
				IFileReader fileReader = new FileReader(new Helpers.FileAccess(), new LineCounting());
				var files = (await fileReader.ReadFilesAsync(settings)).ToList();
				AnsiConsole.MarkupLine($"[green]Successfully parsed {files.Count} files[/]");

				ctx.Status("Creating report");

				ILineCountingReportGenerator reportGenerator = new LineCountingReportGenerator();
				report = reportGenerator.CreateReport(files);
				AnsiConsole.MarkupLine("[green]Successfully created report[/]");
			});

		OutputReport(report, settings);

		return 0;
	}

	/// <summary>
	/// Writes the given report to the console
	/// </summary>
	/// <param name="report">line counting report</param>
	/// <param name="settings">settings</param>
	private static void OutputReport(LineCountingReport? report, LineCountSettings settings)
	{
		if (report is null)
		{
			AnsiConsole.MarkupLine("[red]Error: Could not create line counting report[/]");
			return;
		}

		AnsiConsole.Clear();
		AnsiConsole.MarkupLine($"Report for [italic]\"{settings.Directory}\"[/]");
		AnsiConsole.MarkupLine("");

		if (report.Lines != null)
		{
			// Line statistics
			var linesTable = new Table();
			linesTable.AddColumn(new TableColumn("").Footer("[green bold]= Total[/]"));
			linesTable.AddColumn(new TableColumn("").RightAligned().Footer($"[green bold]{report.Lines.TotalLines:n0}[/]"));
			linesTable.AddRow("Code", report.Lines.CodeLines.ToString("n0"));
			linesTable.AddRow("[olive]+[/] Comments", report.Lines.CommentLines.ToString("n0"));
			linesTable.AddRow("[olive]+[/] Empty", report.Lines.EmptyLines.ToString("n0"));
			linesTable.HideHeaders();
			linesTable.ShowFooters = true;

			var linesPanel = new Panel(linesTable)
			{
				Header = new("[teal]Lines[/]"),
				Padding = new(1, 0, 1, 0)
			};

			AnsiConsole.Write(linesPanel);
		}

		if (report.Languages != null)
		{
			// Language statistics
			var languagesTable = new Table()
			{
				ShowHeaders = false
			};
			languagesTable.AddColumn(new TableColumn(""));
			languagesTable.AddColumn(new TableColumn("").RightAligned());

			foreach (var (language, totalLines) in report.Languages.TopLanguages)
			{
				languagesTable.AddRow(language.Name!, totalLines.ToString("n0"));
			}

			var languagesPanel = new Panel(languagesTable)
			{
				Header = new("[teal]Languages[/]"),
				Padding = new(1, 0, 1, 0)
			};

			AnsiConsole.Write(languagesPanel);
		}
	}
}