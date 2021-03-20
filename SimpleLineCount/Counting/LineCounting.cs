using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SimpleLineCount.FileReading;
using SimpleLineCount.Output;

namespace SimpleLineCount.Counting
{
	public class LineCounting : ILineCounting
	{
		private readonly IOutput output;

		public LineCounting(IOutput output)
		{
			this.output = output;
		}

		#region interface ILineCounting

		public int CountLines(List<InputFile> inputFiles)
		{
			int count = 0;

			int numberOfFiles = inputFiles.Count;

			int progress = 0;
			output.SetProgress(progress, numberOfFiles, "Initializing...");

			foreach (var inputFile in inputFiles)
			{
				output.SetProgress(progress++, numberOfFiles, $"Parsing \"{inputFile.FilePath}\"");

				count += CountFileLines(inputFile);
			}

			// TODO: pass result object (number of files, ignored comment lines, ...)
			output.SetResult(count);

			return count;
		}

		#endregion

		#region private methods

		private int CountFileLines(InputFile inputFile)
		{
			return inputFile.Lines.Count(x => !string.IsNullOrWhiteSpace(x));
		}

		#endregion
	}
}