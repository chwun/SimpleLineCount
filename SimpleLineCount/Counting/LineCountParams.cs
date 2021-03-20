namespace SimpleLineCount.Counting
{
	public record LineCountParams(
		bool ScanFilesRecursively,
		FileOptions FileOptions,
		CodeOptions CodeOptions);
}