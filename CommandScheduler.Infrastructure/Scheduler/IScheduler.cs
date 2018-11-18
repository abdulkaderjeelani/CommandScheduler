using CommandScheduler.Application.DataModel;

namespace CommandScheduler.Infrastructure.Scheduler
{
    public interface IQuartzCommandScheduler
    {
        void Modify(ScheduledCommand command);
        void Schedule(ScheduledCommand command);
        void Start();
        void Stop();
        void UnSchedule(ScheduledCommand command);
    }
}