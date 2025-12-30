using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NShell;

namespace NShell.Commands
{
    public class IpConfigCommand : CommandBase
    {
        public override string Name => "ipconfig";
        public override string Arguments => "";
        public override string Description => "Display network interfaces and IP information";

        public override void Execute(ShellContext context, string args)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                .ToList();

            if (!interfaces.Any())
            {
                Console.WriteLine("No active network interfaces found.");
                return;
            }

            foreach (var ni in interfaces)
            {
                Console.WriteLine($"Interface: {ni.Name}");
                Console.WriteLine($"  Status: {ni.OperationalStatus}");

                var ipProps = ni.GetIPProperties();

                // IPv4 addresses first
                foreach (var addr in ipProps.UnicastAddresses
                             .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    Console.WriteLine($"  IPv4 Address: {addr.Address}");
                    Console.WriteLine($"  Subnet Mask: {addr.IPv4Mask}");
                }

                // IPv6 addresses (optional)
                foreach (var addr in ipProps.UnicastAddresses
                             .Where(a => a.Address.AddressFamily == AddressFamily.InterNetworkV6))
                {
                    Console.WriteLine($"  IPv6 Address: {addr.Address}");
                }

                // Gateway
                foreach (var gw in ipProps.GatewayAddresses.Where(g => g.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    Console.WriteLine($"  Gateway: {gw.Address}");
                }

                // MAC address
                var mac = ni.GetPhysicalAddress();
                if (mac != null && mac.GetAddressBytes().Length > 0)
                    Console.WriteLine($"  MAC: {BitConverter.ToString(mac.GetAddressBytes())}");

                Console.WriteLine(); 
            }
        }
    }
}