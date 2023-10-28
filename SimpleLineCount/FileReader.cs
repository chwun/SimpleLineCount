using SimpleLineCount.Config;
using SimpleLineCount.Helpers;

namespace SimpleLineCount;

/// <summary>
/// Class for reading all relevant source files
/// </summary>
public class FileReader : IFileReader
{
	private readonly IFileAccess fileAccess;
	private readonly ILineCounting lineCounting;
	private readonly IConfigReader configReader;

	public FileReader(IFileAccess fileAccess, ILineCounting lineCounting, IConfigReader configReader)
	{
		this.fileAccess = fileAccess;
		this.lineCounting = lineCounting;
		this.configReader = configReader;
	}

	/// <summary>
	/// Parses the files specified by the given settings object into an enumerable of source files
	/// </summary>
	/// <param name="settings">settings</param>
	/// <returns>list of source files</returns>
	public async Task<List<SourceFile>> ReadFilesAsync(LineCountSettings settings)
	{
		List<SourceFile> files = new();

		var languages = await GetLanguagesAsync();
		var languageMapping = CreateFileExtensionMapping(languages);

		// TODO: error handling: no languages found

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

				files.Add(sourceFile);
			}
		}

		return files;
	}

	/// <summary>
	/// Returns a list of relevant languages, which should be used in counting
	/// </summary>
	/// <returns>list of relevant languages</returns>
	private async Task<List<SourceFileLanguage>> GetLanguagesAsync()
	{
		return (await configReader.GetConfigAsync()).IncludedLanguages;
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