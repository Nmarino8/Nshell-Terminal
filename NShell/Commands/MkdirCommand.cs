using System;
namespace NShell.Commands;

public class MkdirCommand : CommandBase
{
    public override string Name => "mkdir";
    public override string Arguments => "<dir_name>";
    public override string Description => "Create a directory";
    public override IEnumerable<string> Aliases => Array.Empty<string>();

    public override void Execute(ShellContext context, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine("Usage: mkdir <directory_name>");
            return;
        }

        string path = Path.Combine(context.CurrentDirectory, args);

        if (Directory.Exists(path))
        {
            Console.WriteLine("Directory already exists!");
            return;
        }

        Directory.CreateDirectory(path);
        Console.WriteLine($"Directory '{args}' created.");
    }
}

