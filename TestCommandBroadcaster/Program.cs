using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestCommandBroadcaster
{
    class Program
    {
        static void Main(string[] args)
        {           
            string ip = GetLocalIPv4();

            CommandBroadcastService.CommandBroadcastServiceClient client = new CommandBroadcastService.CommandBroadcastServiceClient();

            while (true)
            {

                var res1 = client.BroadCastCommand(new CommandBroadcastService.CommandInfo
                {
                    IPAddressV4 = ip,
                    CommandText = "dir"
                });

                Console.WriteLine(res1.ErrorMessage);
                Console.ReadLine();
            }

            /*
            var res2 = client.BroadCastCommand(new CommandBroadcastService.CommandInfo
            {
                IPAddressV4 = ip,
                CommandText = "dirty"
            });

            Console.WriteLine(res2.ErrorMessage);
            Console.ReadLine();
            */
        }


        private static string GetLocalIPv4()
        {
            string ip = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            if (string.IsNullOrWhiteSpace(ip))
                ip = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrWhiteSpace(ip))
                ip = GetLocalIPv4(NetworkInterfaceType.Unknown);

            if (string.IsNullOrWhiteSpace(ip))
                throw new NotSupportedException("Could not get ip address");

            return ip;
        }

        private static string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string ipv4 = string.Empty;
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            ipv4 = ip.Address.ToString();

            return ipv4;
        }
    }


}
