namespace SimpleLineCount.Models;

/// <summary>
/// Line count statistics
/// </summary>
public class LineCountStatistics
{
	/// <summary>
	/// Total number of lines
	/// </summary>
	public int TotalLines { get; set; }

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