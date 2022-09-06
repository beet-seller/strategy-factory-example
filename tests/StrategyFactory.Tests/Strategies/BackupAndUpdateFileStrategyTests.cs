using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NUnit.Framework;
using StrategyFactory.Strategies;
using StrategyFactory.Tests.Shared;

namespace StrategyFactory.Tests.Strategies;

[TestFixture]
public sealed class BackupAndUpdateFileStrategyTests
{
    private MockFileSystem? _mockFileSystem;

    [SetUp]
    public void SetUp() => _mockFileSystem = new MockFileSystem();

    [Test]
    public async Task When_strategy_is_invoked_and_backup_directory_exists_file_is_backed_up_and_updated()
    {
        _mockFileSystem!.Directory.CreateDirectory(TestData.BackupPath);

        await SharedTest();
    }

    [Test]
    public async Task When_strategy_is_invoked_and_backup_directory_does_not_exist_file_is_backed_up_and_updated()
    {
        if (_mockFileSystem!.Directory.Exists(TestData.BackupPath))
        {
            _mockFileSystem.Directory.Delete(TestData.BackupPath);
        }

        await SharedTest();
    }

    private async Task SharedTest()
    {
        // arrange
        _mockFileSystem!.AddFile(TestData.Path, TestData.File);

        var subjectUnderTest = new BackupAndUpdateFileStrategy(_mockFileSystem!, TestData.BackupPath);

        // act
        await subjectUnderTest.UpdateAsync(TestData.Path, TestData.UpdatedContent);

        var updatedContent = await _mockFileSystem!.File.ReadAllTextAsync(TestData.Path);
        var originalContent = await _mockFileSystem.File.ReadAllTextAsync($"{TestData.BackupPath}\\{Path.GetFileName(TestData.Path)}");

        // assert
        updatedContent.Should().Be(TestData.UpdatedContent);
        originalContent.Should().Be(TestData.OriginalContent);
    }
}