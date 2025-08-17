using CLI.Services.Store;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

[UsedImplicitly]
internal sealed class SetCommand(
    ValueStore kvStore
) : Command<SetCommand.Settings>
{
#region Settings

    [UsedImplicitly]
    internal sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<KEY>")]
        public string Key { get; set; } = string.Empty;


        [CommandArgument(1, "<VALUE>")]
        public string Value { get; set; } = string.Empty;
    }

#endregion

#region Function

    public override int Execute(
        CommandContext context, Settings settings
    ) {
        kvStore.Upsert(settings.Key, settings.Value);
        AnsiConsole.MarkupLine($"[green]Upsert >> {settings.Key}={settings.Value}[/]");
        return 0;
    }

#endregion
}
