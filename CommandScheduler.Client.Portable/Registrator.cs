using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CommandScheduler.Client.Portable.Interface;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client.Portable
{
    public class Registrator
    {
        readonly ISubscriber Subscriber = null;
        readonly IObserver<ClientCommand> Observer = null;

        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="subscriber">Subscriber for Command Publisher (If wcf pass wcf client, if AMQP pass the listener)</param>
        /// <param name="observer">A listener to process the commands received by the subscriber</param>
        public Registrator(ISubscriber subscriber, IObserver<ClientCommand> observer)
        {

            Observer = observer;
            Subscriber = subscriber;
        }

        public bool Register(EventHandler closeEventHandler)
        {            
            string ipv4 = GetLocalIPv4();

            //check here to fail fast
            if (string.IsNullOrEmpty(ipv4))
                throw new ArgumentOutOfRangeException("IP address cannot be empty");

            bool isSuccess = false;

            //Subscribe the saga to process commands from listener          
            Subscriber.Subscribe(Observer, ipv4, null, null, out isSuccess).Subscribe(p =>
            {
                //unsubscribe when client connection is closed
                Subscriber.UnSubscribe();
                closeEventHandler?.Invoke(p.Sender, p.EventArgs as EventArgs);
            });
            
            return isSuccess;

        }

        public void UnRegister()
        {            
            Subscriber.UnSubscribe();
        }

        private string GetLocalIPv4()
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

        private string GetLocalIPv4(NetworkInterfaceType _type)
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
