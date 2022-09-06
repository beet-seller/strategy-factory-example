using StrategyFactory.Enums;
using StrategyFactory.Factories;

namespace StrategyFactory.Services;

public sealed class UpdateFileService : IUpdateFileService
{
    private readonly IUpdateFileStrategyFactory _updateFileStrategyFactory;

    public UpdateFileService(IUpdateFileStrategyFactory updateFileStrategyFactory) => _updateFileStrategyFactory = updateFileStrategyFactory;

    public Task UpdateAsync(string path, string content, UpdateType updateType)
    {
        var updateFileStrategy = _updateFileStrategyFactory.Create(updateType);

        return updateFileStrategy.UpdateAsync(path, content);
    }
}