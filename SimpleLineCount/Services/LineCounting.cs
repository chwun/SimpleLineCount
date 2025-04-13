using SimpleLineCount.Models;

namespace SimpleLineCount.Services;

/// <summary>
/// Class for line counting
/// </summary>
public class LineCounting : ILineCounting
{
	/// <inheritdoc/>
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

		sourceFile.Statistics = new()
		{
			CodeLines = codeLines,
			CommentLines = commentLines,
			EmptyLines = emptyLines,
			TotalLines = codeLines + commentLines + emptyLines
		};
	}

	/// <summary>
	/// Returns true if the given line is empty or contains only whitespace
	/// </summary>
	/// <param name="line">line</param>
	/// <returns>true if the given line is empty or contains only whitespace</returns>
	private static bool IsEmptyLine(string line)
	{
		return string.IsNullOrWhiteSpace(line);
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment start and end token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment start and end token</returns>
	private static bool IsMultiLineCommentStartAndEnd(string line, SourceFileLanguage language)
	{
		if (!language.MultiLineCommentTokens.HasValue
			|| string.IsNullOrWhiteSpace(language.MultiLineCommentTokens.Value.StartToken)
			|| string.IsNullOrWhiteSpace(language.MultiLineCommentTokens.Value.EndToken))
		{
			return false;
		}

		return line.StartsWith(language.MultiLineCommentTokens.Value.StartToken)
			&& line.EndsWith(language.MultiLineCommentTokens.Value.EndToken);
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment start token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment start token</returns>
	private static bool IsMultiLineCommentStart(string line, SourceFileLanguage language)
	{
		if (!language.MultiLineCommentTokens.HasValue || string.IsNullOrWhiteSpace(language.MultiLineCommentTokens.Value.StartToken))
		{
			return false;
		}

		return line.StartsWith(language.MultiLineCommentTokens.Value.StartToken);
	}

	/// <summary>
	/// Returns true if the given line contains the multi line comment end token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the multi line comment end token</returns>
	private static bool IsMultiLineCommentEnd(string line, SourceFileLanguage language)
	{
		if (!language.MultiLineCommentTokens.HasValue || string.IsNullOrWhiteSpace(language.MultiLineCommentTokens.Value.EndToken))
		{
			return false;
		}

		return line.EndsWith(language.MultiLineCommentTokens.Value.EndToken);
	}

	/// <summary>
	/// Returns true if the given line contains the single line comment token
	/// </summary>
	/// <param name="line">line</param>
	/// <param name="language">language</param>
	/// <returns>true if the given line contains the single line comment token</returns>
	private static bool IsSingleLineComment(string line, SourceFileLanguage language)
	{
		return string.IsNullOrWhiteSpace(language.SingleLineCommentToken)
			? false
			: line.StartsWith(language.SingleLineCommentToken);
	}
}