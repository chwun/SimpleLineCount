using SimpleLineCount.Commands.LineCount;
using SimpleLineCount.Helpers;
using SimpleLineCount.Models;
using SimpleLineCount.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SimpleLineCount.Tests.Commands.LineCount;

public class LineCountCommandTests
{
	private readonly LineCountCommand sut;
	private readonly IAnsiConsole console;
	private readonly IFileAccess fileAccess;
	private readonly IFileReader fileReader;
	private readonly ILineCountingReportGenerator reportGenerator;
	private readonly IReportOutputWriter reportOutputWriter;
	private readonly CommandContext commandContext;

	public LineCountCommandTests()
	{
		console = Substitute.For<IAnsiConsole>();
		fileAccess = Substitute.For<IFileAccess>();
		fileReader = Substitute.For<IFileReader>();
		reportGenerator = Substitute.For<ILineCountingReportGenerator>();
		reportOutputWriter = Substitute.For<IReportOutputWriter>();
		sut = new(console, fileAccess, fileReader, reportGenerator, reportOutputWriter);

		commandContext = new([], Substitute.For<IRemainingArguments>(), "", null);
	}

	#region ExecuteAsync

	[Fact]
	public async Task ExecuteAsync_ValidDirectory_ShouldParseFilesAndGenerateReport()
	{
		// Arrange
		var settings = new LineCountSettings { Directory = "valid_directory" };
		List<SourceFile> sourceFiles = [new(), new()];
		fileAccess.DirectoryExists(settings.Directory).Returns(true);
		fileReader.ReadFilesAsync(settings).Returns(sourceFiles);
		LineCountingReport report = new();
		reportGenerator.CreateReport(sourceFiles).Returns(report);

		// Act
		var result = await sut.ExecuteAsync(commandContext, settings);

		// Assert
		result.Should().Be(0);
		await fileReader.Received(1).ReadFilesAsync(settings);
		reportGenerator.Received(1).CreateReport(sourceFiles);
		reportOutputWriter.Received(1).WriteReport(report, settings, sourceFiles.Count, Arg.Any<TimeSpan>());
	}

	[Fact]
	public async Task ExecuteAsync_InvalidDirectory_ShouldReturnError()
	{
		// Arrange
		var settings = new LineCountSettings { Directory = "invalid_directory" };
		fileAccess.DirectoryExists(settings.Directory).Returns(false);

		// Act
		var result = await sut.ExecuteAsync(commandContext, settings);

		// Assert
		result.Should().NotBe(0);
		await fileReader.DidNotReceive().ReadFilesAsync(settings);
		reportGenerator.DidNotReceive().CreateReport(Arg.Any<List<SourceFile>>());
		reportOutputWriter.DidNotReceive().WriteReport(Arg.Any<LineCountingReport>(), settings, Arg.Any<int>(), Arg.Any<TimeSpan>());
	}

	#endregion ExecuteAsync
}