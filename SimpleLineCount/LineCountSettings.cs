using Spectre.Console.Cli;
using System.ComponentModel;

namespace SimpleLineCount;

/// <summary>
/// CLI settings for line counting
/// </summary>
public class LineCountSettings : CommandSettings
{
	//[Description("Directory used for line counting")]
	[CommandArgument(0, "<directory>")]
	public string Directory { get; init; } = "";
}