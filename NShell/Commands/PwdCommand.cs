using System;
namespace NShell.Commands;

public class PwdCommand : CommandBase
{
    public override string Name => "pwd";
    public override string Description => "Print working directory";
    public override IEnumerable<string> Aliases => Array.Empty<string>();

    public override void Execute(ShellContext context, string args)
    {
        var rel = Path.GetRelativePath(
            Directory.GetCurrentDirectory(),
            context.CurrentDirectory
        );

        Console.WriteLine("NShell/" + rel.Replace("\\", "/"));
    }
}

