using CommandScheduler.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Core.Domain.ValueObjects
{
    /// <summary>
    /// A schedule without a command is meaning less in current system,
    /// If we need to identify schedule separately then we can treat this VO as a Entity as it has own identity in the system.
    /// Improvements to system: Create schedules separately and map them to command as per DRY and improve the UX
    /// </summary>
    public class ScheduleInstruction : ValueObject<ScheduleInstruction>
    {
        /// <summary>
        /// Simple, Daily, Calendar, Cron
        /// </summary>
        public string ScheduleType { get; set; }

        //Simple 
        public int IntervalInMinutes { get; set; }
        public int TimesToExecute { get; set; }

        //Daily 
        public DayOfWeek[] RunOnDays { get; set; }
        public int DailyCount { get; set; }

        //Calendar
        public int IntervalInDays { get; set; }

        //Cron
        public string CronExpression { get; set; }

        public bool StartNow { get; set; }

        /// <summary>
        /// Only meaningful if StartNow is false
        /// </summary>
        public DateTimeOffset StartAt { get; set; }

    }
}
