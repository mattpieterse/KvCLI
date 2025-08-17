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

#region Function

    public override int Execute(
        CommandContext context, Settings settings
    ) {
        kvStore.Delete(settings.Key);
        AnsiConsole.MarkupLine($"[green]Delete >> {settings.Key}[/]");
        return 0;
    }

#endregion
}
