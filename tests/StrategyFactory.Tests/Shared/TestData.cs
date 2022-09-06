using System.IO.Abstractions.TestingHelpers;

namespace StrategyFactory.Tests.Shared;

public static class TestData
{
    public const string Path = "foo\\bar\\baz.txt";
    public const string BackupPath = "backups";
    public const string OriginalContent = "Hello World!";
    public const string UpdatedContent = "Hello Universe!";
    public static MockFileData File => new(OriginalContent);
}