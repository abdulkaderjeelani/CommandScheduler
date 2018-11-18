using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;
using CommandScheduler.Core.Domain.Entities;

namespace CommandScheduler.Core.Domain.Events
{
    public class ClientRegistered : Event
    {
        public ClientRegistered(ClientMachine clientMachine)
        {
            ClientMachine = clientMachine;
        }
        public ClientMachine ClientMachine { get; private set; }        
        public int? ExistClientId { get; set; } = null;
    }
}
