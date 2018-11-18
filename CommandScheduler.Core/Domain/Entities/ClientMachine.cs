using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;
using CommandScheduler.Core.Domain.ValueObjects;
using CommandScheduler.SharedKernel.Exceptions;

namespace CommandScheduler.Core.Domain.Entities
{
    public class ClientMachine : Entity<int>
    {
        public ClientMachine()
        {

        }
        public ClientMachine(int id) : base(id)
        {

        }

        public string IPv4 { get; set; }
        public string MachineName { get; set; }
        public string OperatingSystemName { get; set; }
      
        public OperatingSystemNames OperatingSystem => (OperatingSystemNames)Enum.Parse(typeof(OperatingSystemNames), OperatingSystemName);

        protected override void Validate()
        {
            try { var test = OperatingSystem; }
            catch { throw new EntityValidationException(new List<string> { "Invalid operating system name" /*todo use i3 here*/ }); }
        }

    }
}
