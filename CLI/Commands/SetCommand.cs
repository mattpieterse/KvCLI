using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace CLI.Commands;

internal sealed class SetCommand : Command<SetCommand.Settings>
{
#region Settings

    [UsedImplicitly]
    internal sealed class Settings : CommandSettings { }

#endregion

#region Function

    public override int Execute(CommandContext context, Settings settings)
        => throw new NotImplementedException();

#endregion
}
