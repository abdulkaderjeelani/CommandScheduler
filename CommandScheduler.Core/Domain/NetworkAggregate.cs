using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.Core.Domain.Events;
using CommandScheduler.SharedKernel;
using CommandScheduler.SharedKernel.Domain;
using CommandScheduler.Core.Domain.Specifications;
using CommandScheduler.Core.Domain.ValueObjects;

namespace CommandScheduler.Core.Domain
{
    /// <summary>
    /// Network Aggreate to enforce all the invariants corresponds to client.
    /// Now, we have only one network we need not persist this Agg. root, If we need to support multiple network then we can persist this Agg. root
    /// INVARIANTS:
    /// No duplicate device ip in registrations,
    /// No duplicate device name in registrations,  
    /// Os name must be valid
    /// </summary>
    public class NetworkAggregate : Aggregate<NetworkAggregate, Network>,
        IAggHandleEvent<ClientRegistered>,
        IAggHandleEvent<ClientRemoved>

    {
        public const string DefaultNetworkID = "097458E9-5AAC-4FF9-BEEF-4E32225CC91C";

        /// <summary>
        /// If using this constructor for creating a existing network, 
        /// Make sure you 
        /// 1. Replay all events to  bring the aggregate to latest state OR
        /// 2. Use the latest snapshot event
        /// </summary>

        public NetworkAggregate()
        {
            SetDefaultRoot();
            _clients = new List<ClientMachine>();
        }



        /// <summary>
        /// For instant loading, load the client machines, when loading agg. itself. Useful if we are not using Event Sourcing
        /// </summary>        
        /// <param name="clients"></param>
        public NetworkAggregate(IEnumerable<ClientMachine> clients)
        {
            SetDefaultRoot();
            _clients = new List<ClientMachine>(clients);
        }

        /// <summary>
        /// For lazy loading, Agg will call the GetNetworkClients when only required
        /// </summary>        
        /// <param name="clientFactory"></param>
        public NetworkAggregate(IClientFactory clientFactory)
        {
            SetDefaultRoot();
            _clientFactory = clientFactory;
        }

        private void SetDefaultRoot() => AggRoot = new Network(Guid.Parse(NetworkAggregate.DefaultNetworkID));

        private List<ClientMachine> _clients = null;
        IClientFactory _clientFactory = null;
        public IEnumerable<ClientMachine> ClientMachines
        {
            get
            {
                if (_clients == null)
                    if (_clientFactory != null)
                        _clients = new List<ClientMachine>(_clientFactory.GetNetworkClients());
                    else
                        throw new InvalidOperationException("Clients cannot be found !");

                return _clients.AsReadOnly(); //AsReadOnly can't be cast to a list!
            }
            private set
            {
                _clients = (List<ClientMachine>)value;
            }
        }


        protected override NetworkAggregate Self => this;

        public void RegisterClientMachine(ClientMachine machine)
        {
            var @event = new ClientRegistered(machine);
            if (machine.Id > 0)
                @event.ExistClientId = machine.Id;

            ApplyChange(@event);
        }

        public void Handle(ClientRegistered e)
        {
            if (e.ExistClientId.HasValue)
                _clients.Remove(_clients.Single(c => c.Id == e.ExistClientId.Value));

            _clients.Add(e.ClientMachine);

        }

        public void RemoveClientMachine(ClientMachine clientMachine)
        {
            ApplyChange(new ClientRemoved(clientMachine));
        }

        public void Handle(ClientRemoved e)
        {
            _clients.Remove(_clients.Single(c => c.Id == e.ClientMachine.Id));

        }

        protected override IEnumerable<ISpecification<NetworkAggregate>> GetInvariants(Event @event)
        {
            /*Expecting c# 7 swith case to improve this*/

            if (@event is ClientRegistered)
            {
                var cltRegEvt = @event as ClientRegistered;

                yield return new UniqueIPSpecification(cltRegEvt.ClientMachine.IPv4, cltRegEvt.ExistClientId);
                yield return new UniqueNameSpecification(cltRegEvt.ClientMachine.MachineName, cltRegEvt.ExistClientId);

            }
        }
    }
}
