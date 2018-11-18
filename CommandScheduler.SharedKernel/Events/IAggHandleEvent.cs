namespace CommandScheduler.SharedKernel
{
    /// <summary>
    /// Agg. imp this interface to specify what are the events it can handle.
    /// In general Agg. will handle all the events raised in its, 
    /// own command handler
    /// operating methods called from SagaCommandHandler
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IAggHandleEvent<TEvent> where TEvent:Event
    {
        void Handle(TEvent e);
    }
}
