using SimpleLineCount.Helpers;
using System.Text.Json;

namespace SimpleLineCount.Config;

/// <summary>
/// Class for reading configuration
/// </summary>
public class ConfigReader : IConfigReader
{
	private readonly IFileAccess fileAccess;

	private const string configFilename = "config.json";

	private Config? config;

	public ConfigReader(IFileAccess fileAccess)
	{
		this.fileAccess = fileAccess;
	}

	/// <summary>
	/// Gets the configuration object
	/// </summary>
	/// <returns>configuration object</returns>
	public async Task<Config> GetConfigAsync()
	{
		if (config is null)
		{
			await ReadConfigAsync();
			return config ?? new();
		}

		return config;
	}

	/// <summary>
	/// Reads the configuration from a file
	/// </summary>
	private async Task ReadConfigAsync()
	{
		try
		{
			string json = await fileAccess.ReadAllTextAsync(configFilename);
			config = JsonSerializer.Deserialize<Config>(json);
		}
		catch { }
	}
}