using System.IO.Abstractions;

namespace StrategyFactory.Strategies;

internal sealed class BackupAndUpdateFileStrategy : IUpdateFileStrategy
{
    private readonly string _backupPath;
    private readonly IFileSystem _fileSystem;

    public BackupAndUpdateFileStrategy(IFileSystem fileSystem, string? backupPath = null)
    {
        _fileSystem = fileSystem;
        _backupPath = backupPath ?? $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\backups";

        if (!_fileSystem.Directory.Exists(_backupPath))
        {
            _fileSystem.Directory.CreateDirectory(_backupPath);
        }
    }

    public async Task UpdateAsync(string path, string content)
    {
        var originalContent = await _fileSystem.File.ReadAllTextAsync(path);

        await _fileSystem.File.WriteAllTextAsync($"{_backupPath}\\{Path.GetFileName(path)}", originalContent);
        await _fileSystem.File.WriteAllTextAsync(path, content);
    }
}