using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NUnit.Framework;
using StrategyFactory.Enums;
using StrategyFactory.Factories;
using StrategyFactory.Strategies;

namespace StrategyFactory.Tests.Factories;

[TestFixture]
public sealed class UpdateFileStrategyFactoryTests
{
    private IFileSystem? _mockFileSystem;

    [SetUp]
    public void SetUp() => _mockFileSystem = new MockFileSystem();

    [Test]
    [TestCase(UpdateType.Update, typeof(UpdateFileStrategy))]
    [TestCase(UpdateType.BackupAndUpdate, typeof(BackupAndUpdateFileStrategy))]
    public void When_factory_is_invoked_it_returns_expected_strategy(UpdateType updateType, Type expectedType)
    {
        // arrange
        var subjectUnderTest = new UpdateFileStrategyFactory(_mockFileSystem!);

        // act
        var strategy = subjectUnderTest.Create(updateType);

        // assert
        strategy.Should().BeOfType(expectedType);
    }
}