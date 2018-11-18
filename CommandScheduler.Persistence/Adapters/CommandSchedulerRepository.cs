using CommandScheduler.Application.Ports;
using CommandScheduler.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.SharedKernel;
using CommandScheduler.SharedKernel.Repository;
using CommandScheduler.Persistence.Database;

namespace CommandScheduler.Persistence.Adapters
{

    public sealed class CommandSchedulerRepository : ICommandSchedulerRepository, IAggregateRepository<CommandAggregate>
    {
        private readonly ICommandSchedulerDb _database;
        public CommandSchedulerRepository(ICommandSchedulerDb database)
        {

        }

        CommandAggregate IAggregateRepository<CommandAggregate>.Get(Guid id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ClientMachine> ICommandSchedulerRepository.GetAllClients()
        {
            throw new NotImplementedException();
        }

        void ICommandSchedulerRepository.RemoveClientMachine(ClientMachine clientMachine)
        {
            throw new NotImplementedException();
        }

        void IAggregateRepository<CommandAggregate>.Save(Guid id, IEnumerable<Event> events, int expectedVersion)
        {
            throw new NotImplementedException();
        }

        void ICommandSchedulerRepository.SaveClientMachine(ClientMachine clientMachine)
        {
            throw new NotImplementedException();
        }
        
        
    }
}
