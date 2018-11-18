using System;

namespace CommandScheduler.SharedKernel
{
    [Serializable]
    public class Event:IEvent
    {               
        public Event()
        {
            Id = Guid.NewGuid();
        }

        public Event(bool isPublishableToExternal)
        {
            Id = Guid.NewGuid();
            IsPublishableToExternal = isPublishableToExternal;
        }

        public Guid Id { get; private set; }

        public Guid AggregateId { get; set; }

        public int AggregateVersion { get; set; }

        /// <summary>
        /// By default we will publish all the events, If an event is not to be published then we set this to false
        /// </summary>
        public bool IsPublishableToExternal { get; protected set; } = true;

        public bool IsNewEvent { get; set; } = false;
        
    }
        
}
