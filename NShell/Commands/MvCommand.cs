using System;
using System.IO;

namespace NShell.Commands;

public class MvCommand : CommandBase
{
    public override string Name => "mv";
    public override string Arguments => "<source> <destination>";
    public override string Description => "Rename or move a file or directory";

    public override void Execute(ShellContext context, string args)
    {
        var parts = args.Split(' ', 2);

        if (parts.Length < 2)
        {
            Console.WriteLine("Usage: mv <source> <destination>");
            return;
        }

        string source = Path.Combine(context.CurrentDirectory, parts[0]);
        string destination = Path.Combine(context.CurrentDirectory, parts[1]);

        try
        {
            if (File.Exists(source))
            {
                File.Move(source, destination);
            }
            else if (Directory.Exists(source))
            {
                Directory.Move(source, destination);
            }
            else
            {
                Console.WriteLine("Source not found.");
                return;
            }

            Console.WriteLine($"Moved '{parts[0]}' → '{parts[1]}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Move failed: {ex.Message}");
        }
    }
}