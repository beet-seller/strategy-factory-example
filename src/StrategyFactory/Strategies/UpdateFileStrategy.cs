using System.IO.Abstractions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StrategyFactory.Tests")]

namespace StrategyFactory.Strategies;

internal sealed class UpdateFileStrategy : IUpdateFileStrategy
{
    private readonly IFileSystem _fileSystem;

    public UpdateFileStrategy(IFileSystem fileSystem) => _fileSystem = fileSystem;

    public Task UpdateAsync(string path, string content) => _fileSystem.File.WriteAllTextAsync(path, content);
}