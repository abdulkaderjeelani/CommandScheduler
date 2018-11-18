using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Infrastructure.Server.CommandService));
            host.Description.Endpoints[2].Contract.ContractBehaviors.Add(new ServiceMetadataContractBehavior(true));
            host.Open();

            Console.WriteLine("server is open");
            Console.ReadLine();

        }
    }
}
