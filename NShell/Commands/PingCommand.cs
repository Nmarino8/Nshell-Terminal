using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace NShell.Commands
{
    public class PingCommand : CommandBase
    {
        public override string Name => "ping";
        public override IEnumerable<string> Aliases => Array.Empty<string>();
        public override string Arguments => "<host>";
        public override string Description => "Ping a host multiple times";

        public override void Execute(ShellContext context, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.WriteLine("Usage: ping <host>");
                return;
            }

            string[] parts = args.Split(' ', 2);
            string host = parts[0];
            int count = -1;

            if (parts.Length > 1 && int.TryParse(parts[1], out int n))
                count = n;

            bool stop = false;

            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                stop = true;
                Console.WriteLine("\nPing stopped by user.");
            };

            try
            {
                using (var ping = new Ping())
                {
                    Console.WriteLine($"Pinging {host} ... (Press Ctrl+C to stop)");

                    int sent = 0;
                    while ((count == -1 || sent < count) && !stop)
                    {
                        PingReply reply = ping.Send(host, 1000); 

                        if (reply.Status == IPStatus.Success)
                            Console.WriteLine($"Reply from {reply.Address}: time={reply.RoundtripTime}ms");
                        else
                            Console.WriteLine($"Ping failed: {reply.Status}");

                        sent++;
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.CancelKeyPress -= null;
            }
        }
    }
}