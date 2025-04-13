using NSubstitute.ExceptionExtensions;
using SimpleLineCount.Config;
using SimpleLineCount.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLineCount.Tests.Config;

public class ConfigReaderTests
{
	private readonly ConfigReader sut;
	private readonly IFileAccess fileAccess;

	public ConfigReaderTests()
	{
		fileAccess = Substitute.For<IFileAccess>();
		sut = new(fileAccess);
	}

	[Fact]
	public async Task GetConfigAsync_ValidConfigFile_ShouldReturnConfig()
	{
		// Arrange
		string configFilePath = "valid_config.json";
		string json = """{"IncludedLanguages":[{"Name": "C#", "FileExtensions": [], "SingleLineCommentToken": "//", "MultiLineCommentTokens": {"StartToken": "/*", "EndToken": "*/"}}], "ExcludedDirectories": ["test"]}""";
		fileAccess.ReadAllTextAsync(configFilePath).Returns(json);

		// Act
		var result = await sut.GetConfigAsync(configFilePath);

		// Assert
		result.Should().NotBeNull();
		result.IncludedLanguages.Should().ContainSingle();
		result.ExcludedDirectories.Should().ContainSingle("test");
		await fileAccess.Received(1).ReadAllTextAsync(configFilePath);
	}

	[Fact]
	public async Task GetConfigAsync_InvalidConfigFile_ShouldReturnDefaultConfig()
	{
		// Arrange
		string configFilePath = "invalid_config.json";
		fileAccess.ReadAllTextAsync(configFilePath).ThrowsAsync<Exception>();
		// Act
		var result = await sut.GetConfigAsync(configFilePath);
		// Assert
		result.Should().NotBeNull();
		result.IncludedLanguages.Should().BeEmpty();
		result.ExcludedDirectories.Should().BeEmpty();
		await fileAccess.Received(1).ReadAllTextAsync(configFilePath);
	}

	[Fact]
	public async Task GetConfigAsync_ValidConfigFile_ShouldReturnCachedConfig()
	{
		// Arrange
		string configFilePath = "valid_config.json";
		string json = """{"IncludedLanguages":[{"Name": "C#", "FileExtensions": [], "SingleLineCommentToken": "//", "MultiLineCommentTokens": {"StartToken": "/*", "EndToken": "*/"}}], "ExcludedDirectories": ["test"]}""";
		fileAccess.ReadAllTextAsync(configFilePath).Returns(json);

		// Act
		var result1 = await sut.GetConfigAsync(configFilePath);
		var result2 = await sut.GetConfigAsync(configFilePath);

		// Assert
		result1.Should().BeSameAs(result2);
		await fileAccess.Received(1).ReadAllTextAsync(configFilePath);
	}
}