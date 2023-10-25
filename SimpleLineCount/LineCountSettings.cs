using Spectre.Console.Cli;
using System.ComponentModel;

namespace SimpleLineCount
{
	/// <summary>
	/// CLI settings for line counting
	/// </summary>
	internal class LineCountSettings : CommandSettings
	{
		[Description("Directory used for line counting")]
		[CommandArgument(0, "<directory>")]
		public string Directory { get; init; } = "";
	}
}