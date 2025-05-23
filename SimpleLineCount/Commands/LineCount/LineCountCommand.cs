using SimpleLineCount.Helpers;
using SimpleLineCount.Models;
using SimpleLineCount.Services;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics;

namespace SimpleLineCount.Commands.LineCount;

/// <summary>
/// Command implementation for line counting
/// </summary>
internal class LineCountCommand(IAnsiConsole console, IFileAccess fileAccess, IFileReader fileReader, ILineCountingReportGenerator reportGenerator, IReportOutputWriter reportOutputWriter)
	: AsyncCommand<LineCountSettings>
{
	/// <inheritdoc />
	public override async Task<int> ExecuteAsync(CommandContext context, LineCountSettings settings)
	{
		if (!fileAccess.DirectoryExists(settings.Directory))
		{
			console.MarkupLine("[red]Error: Invalid directory[/]");
			return 1;
		}

		LineCountingReport? report = null;
		int numberOfFiles = 0;
		Stopwatch sw = Stopwatch.StartNew();

		await AnsiConsole
			.Status()
			.Spinner(Spinner.Known.Dots)
			.SpinnerStyle(Style.Parse("green"))
			.StartAsync("Parsing files", async ctx =>
			{
				var files = await fileReader.ReadFilesAsync(settings);
				numberOfFiles = files.Count;
				console.MarkupLineInterpolated($"[green]Successfully parsed {files.Count} files[/]");

				ctx.Status("Creating report");

				report = reportGenerator.CreateReport(files);
			});

		sw.Stop();
		TimeSpan duration = sw.Elapsed;

		reportOutputWriter.WriteReport(report, settings, numberOfFiles, duration);

		return 0;
	}
}