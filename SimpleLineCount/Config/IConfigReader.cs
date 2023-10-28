namespace SimpleLineCount.Config;

/// <summary>
/// Interface for reading configuration
/// </summary>
public interface IConfigReader
{
	/// <summary>
	/// Gets the configuration object
	/// </summary>
	/// <returns>configuration object</returns>
	Task<Config> GetConfigAsync();
}