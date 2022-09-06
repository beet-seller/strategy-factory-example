namespace StrategyFactory.Strategies;

public interface IUpdateFileStrategy
{
    Task UpdateAsync(string path, string content);
}