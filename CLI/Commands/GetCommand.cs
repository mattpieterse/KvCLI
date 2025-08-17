using CLI.Services.Store;
using JetBrains.Annotations;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CLI.Commands;

[UsedImplicitly]
internal sealed class GetCommand(
    ValueStore kvStore
) : Command<GetCommand.Settings>
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
        var value = kvStore.FetchOne(settings.Key);
        AnsiConsole.MarkupLineInterpolated((value == null)
            ? (FormattableString) $"[red]Could not find a value associated with [{settings.Key}][/]"
            : (FormattableString) $"[green]FetchOne >> {settings.Key}=[{value}][/]");

        return 0;
    }

#endregion
}
