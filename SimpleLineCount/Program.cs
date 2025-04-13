using Microsoft.Extensions.DependencyInjection;
using SimpleLineCount.Config;
using SimpleLineCount.Helpers;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using System.Globalization;

namespace SimpleLineCount;

/// <summary>
/// Program class
/// </summary>
public static class Program
{
	/// <summary>
	/// Main entry point
	/// </summary>
	/// <param name="args">command line args</param>
	/// <returns>exit code</returns>
	public static async Task<int> Main(string[] args)
	{
		var services = new ServiceCollection();
		AddServices(services);

		using var registrar = new DependencyInjectionRegistrar(services);

		var app = new CommandApp<LineCountCommand>(registrar);
		app.Configure(app =>
		{
			app.SetApplicationCulture(CultureInfo.InvariantCulture);
		});

		return await app.RunAsync(args);
	}

	/// <summary>
	/// Adds services to the service collection
	/// </summary>
	/// <param name="services">service collection</param>
	private static void AddServices(IServiceCollection services)
	{
		services.AddSingleton<IFileAccess, Helpers.FileAccess>();
		services.AddSingleton<IFileReader, FileReader>();
		services.AddSingleton<ILineCounting, LineCounting>();
		services.AddSingleton<IConfigReader, ConfigReader>();
		services.AddSingleton<ILineCountingReportGenerator, LineCountingReportGenerator>();
	}
}