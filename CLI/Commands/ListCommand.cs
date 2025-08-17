using CLI.Services.Serialization;
using CLI.Services.Store;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

[UsedImplicitly]
internal sealed class ListCommand(
    ValueStore kvStore,
    IKeyValueSerializer serializer
) : Command<ListCommand.Settings>
{
#region Settings

    [UsedImplicitly]
    internal sealed class Settings : CommandSettings { }

#endregion

#region ExitCode

    private enum ExitCode
    {
        Success = 0,
        Failure = 1,
    }

#endregion

#region Function

    public override int Execute(
        CommandContext context, Settings settings
    ) {
        var secrets = kvStore.FetchAll();
        var product = serializer.Serialize(secrets.ToDictionary());
        AnsiConsole.WriteLine(product);
        return (int) ExitCode.Success;
    }

#endregion
}
