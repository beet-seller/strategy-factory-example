using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NUnit.Framework;
using StrategyFactory.Strategies;
using StrategyFactory.Tests.Shared;

namespace StrategyFactory.Tests.Strategies;

[TestFixture]
public sealed class UpdateFileStrategyTests
{
    private MockFileSystem? _mockFileSystem;

    [SetUp]
    public void SetUp() => _mockFileSystem = new MockFileSystem();

    [Test]
    public async Task When_strategy_is_invoked_file_is_updated()
    {
        // arrange
        _mockFileSystem!.AddFile(TestData.Path, TestData.File);

        var subjectUnderTest = new UpdateFileStrategy(_mockFileSystem!);

        // act
        await subjectUnderTest.UpdateAsync(TestData.Path, TestData.UpdatedContent);

        var updatedContent = await _mockFileSystem!.File.ReadAllTextAsync(TestData.Path);

        // assert
        updatedContent.Should().Be(TestData.UpdatedContent);
    }
}