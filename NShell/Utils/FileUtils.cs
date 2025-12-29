using System;
namespace NShell.Utils;

public static class FileUtils
{
    public static void ListDirectory(string currentDir, string target)
    {
        string path = string.IsNullOrWhiteSpace(target)
            ? currentDir
            : Path.Combine(currentDir, target);

        if (!Directory.Exists(path))
        {
            Console.WriteLine("Directory not found.");
            return;
        }

        foreach (var dir in Directory.GetDirectories(path))
            Console.WriteLine("[DIR] " + Path.GetFileName(dir));

        foreach (var file in Directory.GetFiles(path))
            Console.WriteLine(Path.GetFileName(file));
    }
}

