using System;
using NShell.Utils;

namespace NShell.Commands;

public class LsCommand : CommandBase
{
    public override string Name => "ls";
    public override string Description => "List directory contents";
    public override IEnumerable<string> Aliases => Array.Empty<string>();

    public override void Execute(ShellContext context, string args)
    {
        FileUtils.ListDirectory(context.CurrentDirectory, args);
    }
}