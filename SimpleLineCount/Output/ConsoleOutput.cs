using System;

namespace SimpleLineCount.Output
{
	public class ConsoleOutput : IOutput
	{
		private readonly Action<string> consoleWriteLineCallback;
		private readonly Action consoleClearCallback;

		public ConsoleOutput(Action<string> consoleWriteLineCallback, Action consoleClearCallback)
		{
			this.consoleWriteLineCallback = consoleWriteLineCallback;
			this.consoleClearCallback = consoleClearCallback;
		}

		public void SetProgress(int progress, int total, string statusText)
		{
			float percentProgress = (float)progress / (float)total * 100f;

			consoleClearCallback();

			consoleWriteLineCallback(statusText);
			consoleWriteLineCallback($"{percentProgress:0.0} %");
		}

		public void SetResult(int totalNumberOfLines)
		{
			consoleClearCallback();

			consoleWriteLineCallback($"Found {totalNumberOfLines} lines of code");
		}
	}
}