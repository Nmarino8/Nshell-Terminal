using System;
using System.IO;

namespace NShell.Commands;

public class RmCommand : CommandBase
{
    public override string Name => "rm";
    public override string Arguments => "<file>";
    public override string Description => "Delete a file";

    public override void Execute(ShellContext context, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine("Usage: rm <file>");
            return;
        }

        string path = Path.Combine(context.CurrentDirectory, args);

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            File.Delete(path);
            Console.WriteLine($"Deleted file '{args}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }
}