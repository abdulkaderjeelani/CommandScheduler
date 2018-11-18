using System.Collections;

namespace CommandScheduler.SharedKernel
{
    /// <summary>
    /// If an aggregate handles its own command using the AggCommandHandler, 
    /// Then the agg must implement this for all the command it handles
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IAggHandleCommand<TCommand> where TCommand : Command
    {
        void Execute(TCommand c);
    }
}
