using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Events
{
    public interface IEventPublisher
    {
        void PublishEvents(IEnumerable<Event> events);
    }
}
