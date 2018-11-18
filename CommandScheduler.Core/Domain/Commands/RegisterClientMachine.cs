using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;
using CommandScheduler.Core.Domain.ValueObjects;

namespace CommandScheduler.Core.Domain.Commands
{
    public class RegisterClientMachine : Command
    {
        public RegisterClientMachine(string iPv4, string machineName, string operatingSystemName, int? clientId = null)
        {
            IPv4 = IPv4;
            MachineName = machineName;
            OperatingSystemName = operatingSystemName;
            ClientId = clientId;
        }

        public string IPv4 { get; private set; }
        public string MachineName { get; private set; }
        public string OperatingSystemName { get; private set; }
        public int? ClientId { get; private set; }
        
    }
}
