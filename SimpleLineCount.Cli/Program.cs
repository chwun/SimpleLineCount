using System;
using System.Collections.Generic;
using SimpleLineCount.Counting;
using SimpleLineCount.FileReading;
using SimpleLineCount.Output;

namespace SimpleLineCount.Cli
{
	public class Program
	{
		static public void Main(string[] args)
		{
			IFileAccess fileAccess = new FileAccess();
			IFileReader fileReader = new FileReader(fileAccess);

			// TODO: read from console params
			string directory = @"D:\Entwicklung\HomeAPI\Backend\HomeAPI.Backend";
			LineCountParams lineCountParams = new LineCountParams(
				true,
				new FileOptions(new List<string>() { ".cs" }),
				new CodeOptions(true, true, "//", "/*", "*/")
			);
			// ------------------------------

			var inputFiles = fileReader.ReadFiles(directory, lineCountParams);

			IOutput consoleOutput = new ConsoleOutput(ConsoleWriteLine, ConsoleClear);
			ILineCounting lineCounting = new LineCounting(consoleOutput);

			lineCounting.CountLines(inputFiles);
		}

		private static void ConsoleClear()
		{
			Console.Clear();
		}

		private static void ConsoleWriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}
}
