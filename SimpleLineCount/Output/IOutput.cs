namespace SimpleLineCount.Output
{
	public interface IOutput
	{
		void SetProgress(int progress, int total, string statusText);

		void SetResult(int totalNumberOfLines);
	}
}