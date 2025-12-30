using System;
using System.Linq;

namespace NShell.Commands;

public class HelpCommand : CommandBase
{
    public override string Name => "help";
    public override IEnumerable<string> Aliases => new[] { "-h" };
    public override string Arguments => "";
    public override string Description => "Show available commands";

    public override void Execute(ShellContext context, string args)
    {
        Console.WriteLine("Available commands:\n");

        var commands = context.Commands.Values.Distinct().ToList();

        int colWidth = 25;

        foreach (var cmd in commands)
        {
            string aliasString = cmd.Aliases.Any() ? $" ({string.Join(", ", cmd.Aliases)})" : "";
            string nameWithAlias = cmd.Name + aliasString;

            if (!string.IsNullOrEmpty(cmd.Arguments))
                nameWithAlias += " " + cmd.Arguments;

            Console.WriteLine($"  {nameWithAlias.PadRight(colWidth)}{cmd.Description}");
        }
    }
}