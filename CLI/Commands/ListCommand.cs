using Spectre.Console.Cli;

namespace CLI.Commands;

internal sealed class ListCommand : Command<ListCommand.Settings>
{
#region Settings

    internal abstract class Settings : CommandSettings { }

#endregion

#region Function

    public override int Execute(CommandContext context, Settings settings)
        => throw new NotImplementedException();

#endregion
}
