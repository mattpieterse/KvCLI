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

#region Function

    public override int Execute(
        CommandContext context, Settings settings
    ) {
        var secrets = kvStore.FetchAll();
        var product = serializer.Serialize(secrets.ToDictionary());
        AnsiConsole.WriteLine(product);
        return 0;
    }

#endregion
}
