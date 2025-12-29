using System;
namespace NShell.Commands
{
	public class ClearCommand:CommandBase
	{
		public override string Name => "clear";
		public override IEnumerable<string> Aliases => new[] { "clr" };
		public override string Arguments => "";
		public override string Description => "Clear the console screen";

        public override void Execute(ShellContext context, string args)
        {
			Console.Clear();
        }
    }
}

