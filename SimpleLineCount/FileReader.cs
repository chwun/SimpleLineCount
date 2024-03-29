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

		var config = await configReader.GetConfigAsync();
		var languages = config.IncludedLanguages;
		var languageMapping = CreateFileExtensionMapping(languages);
		var excludedDirectories = config.ExcludedDirectories;

		// TODO: error handling: no languages found

		foreach (string file in fileAccess.DirectoryEnumerateFiles(settings.Directory))
		{
			if (excludedDirectories.Count > 0 && FileNameContainsExcludedDirectoryName(file, excludedDirectories))
			{
				continue;
			}

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

	/// <summary>
	/// Checks if the given file name contains an excluded directory name
	/// </summary>
	/// <param name="file">file name</param>
	/// <param name="excludedDirectories">set of excluded directory names</param>
	/// <returns>true if file name contains an excluded directory name</returns>
	private static bool FileNameContainsExcludedDirectoryName(string file, HashSet<string> excludedDirectories)
	{
		try
		{
			DirectoryInfo? currentDirectory = Directory.GetParent(file);
			while (currentDirectory != null)
			{
				if (excludedDirectories.Contains(currentDirectory.Name))
				{
					return true;
				}

				currentDirectory = Directory.GetParent(currentDirectory.FullName);
			}
		}
		catch { }

		return false;
	}
}