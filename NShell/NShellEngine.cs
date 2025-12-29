using System;
using NShell.Commands;
using NShell.Utils;

namespace NShell;

public class NShellEngine
{
    private readonly ShellContext _context;
    private readonly Dictionary<string, CommandBase> _commands;

    public Dictionary<string, CommandBase> Commands => _commands;

    public NShellEngine()
    {
        string root = Path.Combine(Directory.GetCurrentDirectory(), "Root");
        Directory.CreateDirectory(root);

        _context = new ShellContext(root);
        _commands = CommandRegistry.RegisterAll();
        _context.Commands = _commands;
    }

    public void Run()
    {
        ConsoleUtils.PrintWelcome();

        while (_context.IsRunning)
        {
            ConsoleUtils.PrintPrompt(_context);
            string input = ConsoleUtils.ReadLineWithHistory(_context);
            Execute(input);
        }
    }

    private void Execute(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return;

        var parts = input.Split(' ', 2);
        var cmdName = parts[0].ToLower();
        var args = parts.Length > 1 ? parts[1] : "";

        if (_commands.TryGetValue(cmdName, out var command))
        {
            command.Execute(_context, args);
        }
        else
        {
            Console.WriteLine($"Command not found: {cmdName}");
        }
    }

    private string GetVirtualPath()
    {
        var rel = Path.GetRelativePath(
            Directory.GetCurrentDirectory(),
            _context.CurrentDirectory
        );

        return "NShell/" + rel.Replace("\\", "/");
    }
}