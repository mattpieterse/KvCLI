using CLI.Services.Store;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

[UsedImplicitly]
internal sealed class ClearCommand(
    ValueStore kvStore
) : Command<ClearCommand.Settings>
{
#region Settings

    [UsedImplicitly]
    internal sealed class Settings : CommandSettings { }

#endregion

#region Function

    public override int Execute(
        CommandContext context, Settings settings
    ) {
        kvStore.Destroy();
        AnsiConsole.MarkupLine("[green]KeyStore wiped successfully.[/]");
        return 0;
    }

#endregion
}
