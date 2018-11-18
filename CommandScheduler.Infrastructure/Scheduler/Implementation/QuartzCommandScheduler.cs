using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using CommandScheduler.Application.DataModel;

namespace CommandScheduler.Infrastructure.Scheduler.Implementation
{
    /// <summary>
    /// Scheduler implementation with Quartz .NET
    /// TODO: If needed move this to spearate library
    /// </summary>
    public class QuartzCommandScheduler : IQuartzCommandScheduler
    {
        /// <summary>
        /// Make sure that the configuration file contains, valid quartz section.
        /// Refer quartz.config
        /// </summary>
        /// <returns></returns>
        public static QuartzCommandScheduler Create()
        {
            if (Instance == null)
                Instance = new QuartzCommandScheduler();

            return Instance;
        }

        private static QuartzCommandScheduler Instance = null;

        private QuartzCommandScheduler()
        {
            Scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        IScheduler Scheduler { get; set; } = null;
        public void Start()
        {
            Scheduler.Start();
        }

        public void Stop()
        {
            Scheduler.Standby();
        }

        public void Schedule(ScheduledCommand command)
        {
            
        }

        public void Modify(ScheduledCommand command)
        {
            //Scheduler.RescheduleJob()
        }
        public void UnSchedule(ScheduledCommand command)
        {

        }

    }
}
