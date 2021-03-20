using System.Collections.Generic;
using SimpleLineCount.Counting;

namespace SimpleLineCount.FileReading
{
	public interface IFileReader
	{
		InputFile ReadFile(string path);

		List<InputFile> ReadFiles(string directory, LineCountParams lineCountParams);
	}
}