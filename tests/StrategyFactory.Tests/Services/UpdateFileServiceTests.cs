using NSubstitute;
using NUnit.Framework;
using StrategyFactory.Enums;
using StrategyFactory.Factories;
using StrategyFactory.Services;
using StrategyFactory.Strategies;
using StrategyFactory.Tests.Shared;

namespace StrategyFactory.Tests.Services;

[TestFixture]
public sealed class UpdateFileServiceTests
{
    private const UpdateType TestUpdateFileStrategyType = UpdateType.BackupAndUpdate;
    private IUpdateFileStrategyFactory? _mockUpdateFileStrategyFactory;
    private IUpdateFileStrategy? _mockUpdateFileStrategy;

    [SetUp]
    public void SetUp()
    {
        _mockUpdateFileStrategyFactory = Substitute.For<IUpdateFileStrategyFactory>();
        _mockUpdateFileStrategy = Substitute.For<IUpdateFileStrategy>();
        _mockUpdateFileStrategyFactory.Create(Arg.Any<UpdateType>()).Returns(_mockUpdateFileStrategy);
    }

    [Test]
    public async Task When_service_is_invoked_expected_components_are_invoked()
    {
        // arrange
        var subjectUnderTest = new UpdateFileService(_mockUpdateFileStrategyFactory!);

        // act
        await subjectUnderTest.UpdateAsync(TestData.Path, TestData.UpdatedContent, TestUpdateFileStrategyType);

        // assert
        _mockUpdateFileStrategyFactory!.Received(1).Create(TestUpdateFileStrategyType);
        await _mockUpdateFileStrategy!.Received(1).UpdateAsync(TestData.Path, TestData.UpdatedContent);
    }
}