using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using StrategyFactory.Enums;
using StrategyFactory.Factories;
using StrategyFactory.Services;

namespace StrategyFactory.App;

[ExcludeFromCodeCoverage(Justification = "application entry point")]
internal static class Program
{
    // Typically we'd retrieve these variable values from user input.
    // For example purposes, we'll just code them in here for now.
    private const UpdateType UpdateFileStrategyType = UpdateType.BackupAndUpdate;
    private static readonly string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\hello_world.txt";
    private static readonly string UpdateContent = $"Hello Universe! {DateTime.Now}";

    private static readonly IUpdateFileService UpdateFileService = new UpdateFileService(new UpdateFileStrategyFactory(new FileSystem()));

    public static async Task Main() => await UpdateFileService.UpdateAsync(Path, UpdateContent, UpdateFileStrategyType);
}