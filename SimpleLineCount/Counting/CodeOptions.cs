namespace SimpleLineCount.Counting
{
	public record CodeOptions(
		bool IgnoreEmptyLines,
		bool IgnoreCommentLines,
		string SingleLineCommentToken,
		string MultiLineCommentStartToken,
		string MultiLineCommentEndToken);
}