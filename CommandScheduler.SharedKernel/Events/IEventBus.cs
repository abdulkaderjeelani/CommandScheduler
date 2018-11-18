using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.SharedKernel
{
    public interface IEventBus
    {
        /// <summary>
        /// Publish to internal registered handlers and external world (through MessageBus)        
        /// </summary>
        /// <param name="event"></param>
        void Publish(Event @event);
    }
}

/* 
 * External Command received by MessageBus (Command), transformed to Internal Command (inside the ACL)
 * Sent to Internal CommandBus, Executed, Raised Event is Published to Internal and External using CommandBus
 
     */
