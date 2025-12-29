using System;
namespace NShell.Commands;

public class CdCommand : CommandBase
{
    public override string Name => "cd";
    public override string Arguments => "<dir>";
    public override string Description => "Change current directory";
    public override IEnumerable<string> Aliases => Array.Empty<string>();

    public override void Execute(ShellContext context, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
            return;

        string target;

        if (args == "..")
        {
            var parent = Directory.GetParent(context.CurrentDirectory);
            if (parent == null || !parent.FullName.StartsWith(context.RootDirectory))
            {
                Console.WriteLine("Cannot leave Root directory!");
                return;
            }
            target = parent.FullName;
        }
        else if (args == "/")
        {
            target = context.RootDirectory;
        }
        else
        {
            target = Path.Combine(context.CurrentDirectory, args);
            if (!Directory.Exists(target))
            {
                Console.WriteLine("Directory not found.");
                return;
            }
        }

        context.CurrentDirectory = Path.GetFullPath(target);
    }
}

