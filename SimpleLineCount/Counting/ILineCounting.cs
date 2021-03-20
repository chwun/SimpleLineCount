using System.Collections.Generic;
using SimpleLineCount.FileReading;

namespace SimpleLineCount.Counting
{
	public interface ILineCounting
	{
		int CountLines(List<InputFile> inputFiles);
	}
}