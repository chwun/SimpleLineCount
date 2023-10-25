using Spectre.Console;
using Spectre.Console.Cli;

namespace SimpleLineCount
{
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
				OutputErrorMessage("Invalid directory");
				return 1;
			}

			

			return 0;
		}

		/// <summary>
		/// Writes the given error message to the console
		/// </summary>
		/// <param name="message">error message</param>
		private void OutputErrorMessage(string message)
		{
			AnsiConsole.MarkupLine($"[red]Error: {message}[/]");
		}
	}
}