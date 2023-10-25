namespace SimpleLineCount;

/// <summary>
/// Line count statistics
/// </summary>
internal class LineCountStatistics
{
	/// <summary>
	/// Number of code lines
	/// </summary>
	public int CodeLines { get; set; }

	/// <summary>
	/// Number of comment lines
	/// </summary>
	public int CommentLines { get; set; }

	/// <summary>
	/// Number of empty lines
	/// </summary>
	public int EmptyLines { get; set; }
}