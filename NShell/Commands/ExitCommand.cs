using System;
namespace NShell.Commands;

public class ExitCommand : CommandBase
{
    public override string Name => "exit";
    public override IEnumerable<string> Aliases => new[] { "-q" };
    public override string Arguments => "";
    public override string Description => "Exit NShell";

    public override void Execute(ShellContext context, string args)
    {
        context.IsRunning = false;
    }
}

