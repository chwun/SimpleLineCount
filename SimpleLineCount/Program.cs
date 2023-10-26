using Spectre.Console.Cli;

namespace SimpleLineCount.Cli;

public class Program
{
	public static async Task<int> Main(string[] args)
	{
		var app = new CommandApp<LineCountCommand>();
		return await app.RunAsync(args);
	}
}