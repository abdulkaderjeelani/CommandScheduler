using CommandScheduler.SharedKernel.Events;
using CommandScheduler.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Repository
{
    public class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate>
        where TAggregate : Aggregate, new()

    {
        private readonly IEventStore _eventStore;
        private static object _lockStorage = new object();

        public AggregateRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }


        public TAggregate Get(Guid id)
        {
            var agg = new TAggregate();
            agg.SetId(id);
            agg.LoadsFromHistory(_eventStore.LoadEventsFor(id, agg.Key));
            /*TODO Memento*/
            return agg;
        }

        public void Save(Guid id, IEnumerable<Event> events, int expectedVersion)
        {
            if (events.Any())
            {
                lock (_lockStorage)
                {
                    var key = new TAggregate().Key;
                    if (expectedVersion != -1)
                        if (_eventStore.GetLatestVersionOf(id, key) != expectedVersion)
                            throw new ConcurrencyException(string.Format("Aggregate {0} has been previously modified", id));

                    _eventStore.SaveEventsFor(id, events, expectedVersion, key);
                }
            }

        }
    }
}
