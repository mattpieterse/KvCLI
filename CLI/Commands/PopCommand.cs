using CLI.Services.Store;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

[UsedImplicitly]
internal sealed class PopCommand(
    ValueStore kvStore
) : Command<PopCommand.Settings>
{
#region Settings

    [UsedImplicitly]
    internal sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<KEY>")]
        public string Key { get; set; } = string.Empty;
    }

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
        kvStore.Delete(settings.Key);
        AnsiConsole.MarkupLine($"[green]Delete >> {settings.Key}[/]");
        return (int) ExitCode.Success;
    }

#endregion
}
