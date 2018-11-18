using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.Core.Domain.Commands
{
    public class RemoveClientMachineRegistration : Command
    {
        public RemoveClientMachineRegistration(int clientId)
        {
            ClientId = clientId;
        }

        public int ClientId { get; private set; }
    }
}
