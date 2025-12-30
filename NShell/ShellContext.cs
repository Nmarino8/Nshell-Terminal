using System;
using NShell.Commands;

namespace NShell;

public class ShellContext
{
    public string RootDirectory { get; }
    public string CurrentDirectory { get; set; }
    public bool IsRunning { get; set; } = true;

    public List<string> History { get; } = new List<string>();
    public int HistoryIndex { get; set; } = -1;

    public Dictionary<string, CommandBase> Commands { get; set; }

    public ShellContext(string rootDir)
    {
        RootDirectory = rootDir;
        CurrentDirectory = rootDir;

        Commands = new Dictionary<string, CommandBase>();
    }
}