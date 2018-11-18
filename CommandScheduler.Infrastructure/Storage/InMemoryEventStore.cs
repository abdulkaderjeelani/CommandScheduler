using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;
using CommandScheduler.SharedKernel.Events;

namespace CommandScheduler.Infrastructure.Storage
{
    public class InMemoryEventStore : IEventStore
    {
        private class Stream
        {
            public List<Event> Events { get; set; } = new List<Event>();
            public int Version { get; set; } = -1;
        }

        private ConcurrentDictionary<Guid, Stream> store = new ConcurrentDictionary<Guid, Stream>();

        public IEnumerable<Event> LoadEventsFor(Guid id, string key = null)
        {              
            Stream s;
            return (store.TryGetValue(id, out s)) ? s.Events : null;
        }

        public void SaveEventsFor(Guid id, IEnumerable<Event> events, int version, string key = null)
        {                     
            var s = store.GetOrAdd(id, _ => new Stream());
           
            foreach (var @event in events)
            {
                version++;
                @event.AggregateVersion = version;
                s.Events.Add(@event);
            }
            //Save the latest version
            s.Version = version;
        }
        
        public int GetLatestVersionOf(Guid id, string key = null)
        {
            return store.GetOrAdd(id, _ => new Stream()).Version;
        }

    }
}
