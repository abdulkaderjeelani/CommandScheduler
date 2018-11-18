using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Entities;

namespace CommandScheduler.Core.Domain
{
    public interface IClientFactory
    {   
        IEnumerable<ClientMachine> GetNetworkClients();
    }
}
