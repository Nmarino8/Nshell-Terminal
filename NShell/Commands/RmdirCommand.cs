using System;
using System.IO;

namespace NShell.Commands;

public class RmdirCommand : CommandBase
{
    public override string Name => "rmdir";
    public override string Arguments => "<folder>";
    public override string Description => "Delete an empty directory";

    public override void Execute(ShellContext context, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            Console.WriteLine("Usage: rmdir <folder>");
            return;
        }

        string path = Path.Combine(context.CurrentDirectory, args);

        if (!Directory.Exists(path))
        {
            Console.WriteLine("Directory not found.");
            return;
        }

        try
        {
            Directory.Delete(path);
            Console.WriteLine($"Deleted directory '{args}'");
        }
        catch (IOException)
        {
            Console.WriteLine("Directory is not empty.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting directory: {ex.Message}");
        }
    }
}