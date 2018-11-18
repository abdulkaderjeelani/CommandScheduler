using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Application.Ports;
using CommandScheduler.Core.Domain;
using CommandScheduler.Core.Domain.Entities;

namespace CommandScheduler.Application.Factories
{
    public class ClientFactory : IClientFactory
    {
        readonly ICommandSchedulerRepository _repository = null;

        public ClientFactory(ICommandSchedulerRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<ClientMachine> GetNetworkClients()
        {
            return _repository.GetAllClients();
        }

        internal static IClientFactory New(ICommandSchedulerRepository repository)
        {
            return new ClientFactory(repository);
        }
    }
}
