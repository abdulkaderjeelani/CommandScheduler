using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Events
{
    /// <summary>
    /// Handlers in Application layer should implement this calss to receive the event. In other words
    /// Any class needs to listen a event should implement this interface.
    /// The framewor should scan the ApplicationLayer and make it subscribe to relevant event
    /// In a MsgBus this event can be captured by any system even if its not implementing this interface. (Taken care of Msg.Bus Mass TRansit)
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent handle);
    }
}
