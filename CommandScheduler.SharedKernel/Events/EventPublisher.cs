using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventBus _eventBus;

        public EventPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void PublishEvents(IEnumerable<Event> events)
        {
            Parallel.ForEach(events, @event =>
            {
                if (@event.IsPublishableToExternal)
                    _eventBus.Publish(@event);
            });
        }
    }
}
