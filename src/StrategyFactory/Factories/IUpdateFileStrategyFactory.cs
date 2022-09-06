using StrategyFactory.Enums;
using StrategyFactory.Strategies;

namespace StrategyFactory.Factories;

public interface IUpdateFileStrategyFactory
{
    IUpdateFileStrategy Create(UpdateType updateType);
}