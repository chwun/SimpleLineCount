using Spectre.Console.Cli;
using System.ComponentModel;

namespace SimpleLineCount.Commands.LineCount;

/// <summary>
/// CLI settings for line counting
/// </summary>
public class LineCountSettings : CommandSettings
{
	[CommandArgument(0, "<directory>")]
	[Description("Directory used for line counting")]
	public string Directory { get; init; } = "";

	[CommandOption("-c|--config")]
	[Description("Path to a custom configuration file (optional)")]
	public string ConfigFilePath { get; init; } = "config.json";
}