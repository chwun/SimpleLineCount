namespace SimpleLineCount.Config;

/// <summary>
/// Interface for reading configuration
/// </summary>
public interface IConfigReader
{
	/// <summary>
	/// Gets the configuration object
	/// </summary>
	/// <param name="configFilePath">configuration file path</param>
	/// <returns>configuration object</returns>
	Task<Config> GetConfigAsync(string configFilePath);
}