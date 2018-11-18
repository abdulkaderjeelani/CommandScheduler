using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel
{
    /// <summary>
    /// Handler in application layer must implement this interface.
    /// When a command is received by the Messaging System (Bus), It will be routed to the Handler.
    /// Usually the class implementing this interface will be a SAGA / Process Manager.
    /// The class should operate on Aggregate, that can generate events, Agg changes its state based on events
    /// IMPORTANT: No change should be made on the Aggregate operations itself (To Support Event Sourcing)
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void Execute(TCommand command);
    }
}
