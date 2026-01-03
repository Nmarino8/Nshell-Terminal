using System;
using System.Net;

namespace NShell.Commands
{
    public class NslookupCommand : CommandBase
    {
        public override string Name => "nslookup";
        public override string Arguments => "<hostname | ip>";
        public override string Description => "Resolve a hostname or IP address";

        public override void Execute(ShellContext context, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.WriteLine("Usage: nslookup <hostname | ip>");
                return;
            }

            if (IPAddress.TryParse(args, out var ip))
            {
                ReverseLookup(ip);
            }
            else
            {
                ForwardLookup(args);
            }
        }

        private void ForwardLookup(string host)
        {
            try
            {
                var addresses = Dns.GetHostAddresses(host);

                Console.WriteLine($"Name: {host}");

                if (addresses.Length == 0)
                {
                    Console.WriteLine("No IP addresses found.");
                    return;
                }

                foreach (var addr in addresses)
                {
                    Console.WriteLine($"Address: {addr}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lookup failed: {ex.Message}");
            }
        }

        private void ReverseLookup(IPAddress ip)
        {
            try
            {
                var entry = Dns.GetHostEntry(ip);

                Console.WriteLine($"Name: {entry.HostName}");
                Console.WriteLine($"Address: {ip}");

                if (entry.Aliases.Length > 0)
                {
                    Console.WriteLine("Aliases:");
                    foreach (var alias in entry.Aliases)
                    {
                        Console.WriteLine($" - {alias}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No reverse DNS record for {ip}: {ex.Message}");
            }
        }
    }
}