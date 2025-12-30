using System.Collections.Generic;
using NShell.Commands;

namespace NShell
{
    public static class CommandRegistry
    {
        public static Dictionary<string, CommandBase> RegisterAll()
        {
            var commands = new List<CommandBase>
            {
                new LsCommand(),
                new CdCommand(),
                new PwdCommand(),
                new MkdirCommand(),
                new TouchCommand(),
                new RmCommand(),
                new RmdirCommand(),
                new MvCommand(),
                new HelpCommand(),
                new ExitCommand(),
                new ClearCommand(),
                new PingCommand(),
                new NslookupCommand(),
                new NanoCommand(),
                new IpConfigCommand()
            };

            var dict = new Dictionary<string, CommandBase>();

            foreach (var cmd in commands)
            {
                dict[cmd.Name] = cmd;

                foreach (var alias in cmd.Aliases)
                {
                    dict[alias] = cmd;
                }
            }

            return dict;
        }
    }
}