namespace SimpleLineCount.Config;

/// <summary>
/// Configuration class
/// </summary>
public class Config
{
	/// <summary>
	/// List of included languages
	/// </summary>
	public List<SourceFileLanguage> IncludedLanguages { get; set; } = new();
}