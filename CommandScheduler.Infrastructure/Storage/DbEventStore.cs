using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;
using CommandScheduler.SharedKernel.Events;

namespace CommandScheduler.Infrastructure.Storage
{
    /// <summary>
    /// Generic database event store
    /// </summary>
    public class DbEventStore : IEventStore
    {
        public int GetLatestVersionOf(Guid id, string key = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> LoadEventsFor(Guid id, string key = null)
        {
            throw new NotImplementedException();
        }

        public void SaveEventsFor(Guid id, IEnumerable<Event> events, int version, string key = null)
        {
            throw new NotImplementedException();
        }
    }
}
