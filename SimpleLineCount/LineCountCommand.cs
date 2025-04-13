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

		ReportOutput reportOutput = new();
		reportOutput.WriteReport(report, settings, numberOfFiles, duration);

		return 0;
	}
}