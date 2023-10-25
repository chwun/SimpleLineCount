namespace SimpleLineCount;

/// <summary>
/// Class for line counting
/// </summary>
internal class LineCounting : ILineCounting
{
	/// <summary>
	/// Counts lines for the given file content
	/// </summary>
	/// <param name="sourceFile">source file</param>
	/// <param name="fileContent">file content as lines</param>
	public void CountLines(SourceFile sourceFile, string[] fileContent)
	{
		int codeLines = 0;
		int commentLines = 0;
		int emptyLines = 0;

		bool multiLineCommentActive = false;

		foreach (string line in fileContent)
		{
			string trimmedline = line.Trim();

			if (IsEmptyLine(trimmedline))
			{
				emptyLines++;
				continue;
			}

			if (IsMultiLineCommentStartAndEnd(trimmedline, sourceFile.Language!))
			{
				commentLines++;
				continue;
			}

			if (IsMultiLineCommentStart(trimmedline, sourceFile.Language!))
			{
				multiLineCommentActive = true;
				commentLines++;
				continue;
			}

			if (IsMultiLineCommentEnd(trimmedline, sourceFile.Language!))
			{
				multiLineCommentActive = false;
				commentLines++;
				continue;
			}

			if (IsSingleLineComment(trimmedline, sourceFile.Language!))
			{
				commentLines++;
				continue;
			}

			if (multiLineCommentActive)
			{
				commentLines++;
				continue;
			}

			codeLines++;
		}
	}

	/// <summary>
	/// Returns true if the given line is empty or contains only whitespace
	/// </summary>
	/// <param name="line">line</param>
	/// <returns>true if the given line is empty or contains only whitespace</returns>
	private bool IsEmptyLine(string line)
	{
		return string.IsNullOrWhiteSpace(line);
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment start and end token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment start and end token</returns>
	private bool IsMultiLineCommentStartAndEnd(string line, SourceFileLanguage language)
	{
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment start token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment start token</returns>
	private bool IsMultiLineCommentStart(string line, SourceFileLanguage language)
	{
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment end token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment end token</returns>
	private bool IsMultiLineCommentEnd(string line, SourceFileLanguage language)
	{
	}

	/// <summary>
	/// Returns true if the given line contains the single line comment token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the single line comment token</returns>
	private bool IsSingleLineComment(string line, SourceFileLanguage language)
	{
	}
}