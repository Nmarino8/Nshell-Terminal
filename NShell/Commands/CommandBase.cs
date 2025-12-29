using System;
namespace NShell.Commands;

public abstract class CommandBase
{
    public abstract string Name { get; }
    public virtual IEnumerable<string> Aliases => Array.Empty<string>();
    public virtual string Arguments => "";
    public abstract string Description { get; }
    public abstract void Execute(ShellContext context, string args);
}