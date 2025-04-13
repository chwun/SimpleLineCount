using SimpleLineCount.Helpers;
using System.Text.Json;

namespace SimpleLineCount.Config;

/// <summary>
/// Class for reading configuration
/// </summary>
public class ConfigReader(IFileAccess fileAccess) : IConfigReader
{
	private readonly IFileAccess fileAccess = fileAccess;

	private Config? config;

	/// <inheritdoc/>
	public async Task<Config> GetConfigAsync(string configFilePath)
	{
		if (config is null)
		{
			await ReadConfigAsync(configFilePath);
			return config ?? new();
		}

		return config;
	}

	/// <summary>
	/// Reads the configuration from a file
	/// </summary>
	/// <param name="configFilePath">configuration file path</param>
	private async Task ReadConfigAsync(string configFilePath)
	{
		try
		{
			string json = await fileAccess.ReadAllTextAsync(configFilePath);
			config = JsonSerializer.Deserialize<Config>(json);
		}
		catch { }
	}
}