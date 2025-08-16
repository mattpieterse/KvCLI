using Spectre.Console.Cli;

namespace CLI.Commands;

internal sealed class PopCommand : Command<PopCommand.Settings>
{
#region Settings

    internal abstract class Settings : CommandSettings { }

#endregion

#region Function

    public override int Execute(CommandContext context, Settings settings)
        => throw new NotImplementedException();

#endregion
}
