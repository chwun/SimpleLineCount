using SimpleLineCount.Helpers;

namespace SimpleLineCount;

/// <summary>
/// Class for reading all relevant source files
/// </summary>
internal class FileReader
{
	private readonly IFileAccess fileAccess;
	private readonly ILineCounting lineCounting;

	public FileReader(IFileAccess fileAccess, ILineCounting lineCounting)
	{
		this.fileAccess = fileAccess;
		this.lineCounting = lineCounting;
	}

	public async IAsyncEnumerable<SourceFile> ReadFilesAsync(LineCountSettings settings)
	{
		var languages = GetLanguages();
		var languageMapping = CreateFileExtensionMapping(languages);

		foreach (string file in fileAccess.DirectoryEnumerateFiles(settings.Directory))
		{
			SourceFile? sourceFile = null;

			try
			{
				string extension = Path.GetExtension(file).ToLower();

				if (!languageMapping.ContainsKey(extension))
				{
					continue;
				}

				sourceFile = new()
				{
					FileName = file,
					Language = languageMapping[extension]
				};
			}
			catch
			{
				// TODO
			}

			if (sourceFile != null)
			{
				string[] content = await fileAccess.ReadAllLinesAsync(sourceFile.FileName);
				lineCounting.CountLines(sourceFile, content);

				yield return sourceFile;
			}
		}
	}

	/// <summary>
	/// Returns a list of relevant languages, which should be used in counting
	/// </summary>
	/// <returns>list of relevant languages</returns>
	private List<SourceFileLanguage> GetLanguages()
	{
		// TODO: read from config file instead!
		return new()
		{
			new()
			{
				Name = "C#",
				FileExtensions = new() { "cs", "csx" },
				SingleLineCommentToken = "//",
				MultiLineCommentTokens = ("/*", "*/")
			}
		};
	}

	/// <summary>
	/// Creates a mapping from file extensions to source file languages
	/// </summary>
	/// <param name="languages">languages</param>
	/// <returns>mapping from file extensions to source file languages</returns>
	private Dictionary<string, SourceFileLanguage> CreateFileExtensionMapping(List<SourceFileLanguage> languages)
	{
		Dictionary<string, SourceFileLanguage> fileExtensionMapping = new();

		foreach (var language in languages)
		{
			foreach (string extension in language.FileExtensions)
			{
				fileExtensionMapping[extension.ToLower()] = language;
			}
		}

		return fileExtensionMapping;
	}
}