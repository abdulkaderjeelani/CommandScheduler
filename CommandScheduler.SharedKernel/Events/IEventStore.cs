using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Events
{
    public interface IEventStore
    {
        IEnumerable<Event> LoadEventsFor(Guid id, string key);
        void SaveEventsFor(Guid id, IEnumerable<Event> events, int version, string key);
        int GetLatestVersionOf(Guid id, string key);
        
    }
}
