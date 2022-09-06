using StrategyFactory.Enums;

namespace StrategyFactory.Services;

public interface IUpdateFileService
{
    Task UpdateAsync(string path, string content, UpdateType updateType);
}