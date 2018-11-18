using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Core.Domain.Specifications
{

    public class UniqueIPSpecification : ISpecification<NetworkAggregate>
    {
        private readonly string _ipv4;
        private readonly int? _existClientId;

        public UniqueIPSpecification(string ipv4, int? existClientId = null)
        {
            _ipv4 = ipv4;
            _existClientId = existClientId;

        }

        public bool IsSatisfiedBy(NetworkAggregate subject) =>
                !((_existClientId.HasValue) ? subject.ClientMachines.Where(c => c.Id != _existClientId.Value) : subject.ClientMachines.AsEnumerable())
                 .Any(c => c.IPv4 == _ipv4);
    }

    public class UniqueNameSpecification : ISpecification<NetworkAggregate>
    {
        private readonly string _machineName;
        private readonly int? _existClientId;

        public UniqueNameSpecification(string machineName, int? existClientId = null)
        {
            _machineName = machineName;
            _existClientId = existClientId;

        }

        public bool IsSatisfiedBy(NetworkAggregate subject) =>
                !((_existClientId.HasValue) ? subject.ClientMachines.Where(c => c.Id != _existClientId.Value) : subject.ClientMachines.AsEnumerable())
                 .Any(c => c.MachineName.Equals(_machineName, StringComparison.InvariantCultureIgnoreCase));
    }


}
