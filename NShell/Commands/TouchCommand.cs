using System;
namespace NShell.Commands;

public class TouchCommand : CommandBase
{
    public override string Name => "touch";
    public override string Arguments => "<file_name>";
    public override string Description => "Create empty file";
    public override IEnumerable<string> Aliases => Array.Empty<string>();

    public override void Execute(ShellContext context, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine("Usage: touch <file_name>");
            return;
        }

        string path = Path.Combine(context.CurrentDirectory, args);

        if (File.Exists(path))
        {
            Console.WriteLine("File already exists!");
            return;
        }

        File.Create(path).Close();
        Console.WriteLine($"File '{args}' created.");
    }
}

