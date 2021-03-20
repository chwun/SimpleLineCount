using System.Collections.Generic;

namespace SimpleLineCount.FileReading
{
	public class InputFile
	{
		public string FilePath { get; }

		public List<string> Lines { get; }

		public InputFile(string filePath, List<string> lines)
		{
			FilePath = filePath;
			Lines = lines;
		}
	}
}