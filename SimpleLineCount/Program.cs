using Spectre.Console.Cli;

namespace SimpleLineCount.Cli;

public class Program
{
	static public async Task<int> Main(string[] args)
	{
		var app = new CommandApp<LineCountCommand>();
		return await app.RunAsync(args);

			// IFileAccess fileAccess = new FileAccess();
			// IFileReader fileReader = new FileReader(fileAccess);

		// // TODO: read from console params
		// string directory = @"D:\Entwicklung\HomeAPI\Backend\HomeAPI.Backend";
		// LineCountParams lineCountParams = new LineCountParams(
		// 	true,
		// 	new FileOptions(new List<string>() { ".cs" }),
		// 	new CodeOptions(true, true, "//", "/*", "*/")
		// );
		// // ------------------------------

		// var inputFiles = fileReader.ReadFiles(directory, lineCountParams);

		// IOutput consoleOutput = new ConsoleOutput(ConsoleWriteLine, ConsoleClear);
		// ILineCounting lineCounting = new LineCounting(consoleOutput);

		// lineCounting.CountLines(inputFiles);
		}


}

