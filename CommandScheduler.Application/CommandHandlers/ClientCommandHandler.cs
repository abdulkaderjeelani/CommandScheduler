using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Application.Factories;
using CommandScheduler.Application.Ports;
using CommandScheduler.Core.Domain;
using CommandScheduler.Core.Domain.Commands;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.SharedKernel;
using CommandScheduler.Utilities;
using CommandScheduler.SharedKernel.Repository;
using CommandScheduler.Core.Domain.Events;

namespace CommandScheduler.Application.CommandHandlers
{
    /// <summary>
    /// Handles client registration commands
    /// </summary>
    public class ClientCommandHandler : ICommandHandler<RegisterClientMachine>, ICommandHandler<RemoveClientMachineRegistration>
    {

        ICommandSchedulerRepository _repository = null;

        public ClientCommandHandler(ICommandSchedulerRepository repository)
        {
            _repository = repository;
        }

        public void Execute(RegisterClientMachine command)
        {
            Guard.ForNull(command);
            NetworkAggregate network = CreateAggregate(); //Create a aggregate using Empty and Load Events (or SnapShot) , or using Factory
            network.RegisterClientMachine(ClientMachineFromCommand(command));
            ExtractEventsToRepository(network.GetUncommittedChanges());
        }

        public void Execute(RemoveClientMachineRegistration command)
        {
            Guard.ForNull(command);
            NetworkAggregate network = CreateAggregate();
            network.RemoveClientMachine(ClientMachineFromCommand(command));
            ExtractEventsToRepository(network.GetUncommittedChanges());
        }
        
        private NetworkAggregate CreateAggregate() => new NetworkAggregate(ClientFactory.New(_repository));// Use DI if neeeded;

        private void ExtractEventsToRepository(IEnumerable<Event> events)
        {
            //Make sure the data are store sequentially, in case of multiple events using OrderBy
            foreach (var @event in events.OrderBy(e => e.AggregateVersion))
            {
                if (@event is ClientRegistered)
                    _repository.SaveClientMachine(ClientMachineFromEvent(@event as ClientRegistered));

                if (@event is ClientRemoved)
                    _repository.RemoveClientMachine(ClientMachineFromEvent(@event as ClientRemoved));
            }
        }

        private ClientMachine ClientMachineFromCommand(RegisterClientMachine command) =>
            new ClientMachine(command.ClientId.HasValue ? command.ClientId.Value : -1)
            {
                IPv4 = command.IPv4,
                MachineName = command.MachineName,
                OperatingSystemName = command.OperatingSystemName

            };
        private ClientMachine ClientMachineFromCommand(RemoveClientMachineRegistration command) => new ClientMachine(command.ClientId);
        private ClientMachine ClientMachineFromEvent(ClientRegistered @event) => @event.ClientMachine;
        private ClientMachine ClientMachineFromEvent(ClientRemoved @event) => @event.ClientMachine;


    }
}
