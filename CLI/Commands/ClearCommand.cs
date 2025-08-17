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
        kvStore.Destroy();
        AnsiConsole.MarkupLine("[green]KeyStore wiped successfully.[/]");
        return (int) ExitCode.Success;
    }

#endregion
}
