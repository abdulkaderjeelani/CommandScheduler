namespace CommandScheduler.Client.Portable.Interface
{
    public interface ISagaDependancyFactory
    {
        ICommandExecutor GetCommandExecutor();
        IResultDispatcher GetResultDispatcher();
    }
}