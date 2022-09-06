using System.IO.Abstractions;
using StrategyFactory.Enums;
using StrategyFactory.Strategies;

namespace StrategyFactory.Factories;

public sealed class UpdateFileStrategyFactory : IUpdateFileStrategyFactory
{
    private readonly string? _backupPath;
    private readonly IFileSystem _fileSystem;

    public UpdateFileStrategyFactory(IFileSystem fileSystem, string? backupPath = null)
    {
        _fileSystem = fileSystem;
        _backupPath = backupPath;
    }

    public IUpdateFileStrategy Create(UpdateType updateType) => updateType switch
    {
        UpdateType.Update => new UpdateFileStrategy(_fileSystem),
        UpdateType.BackupAndUpdate => new BackupAndUpdateFileStrategy(_fileSystem, _backupPath),
        _ => throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null)
    };
}