using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.Core.Domain.ValueObjects;

namespace CommandScheduler.Application.DataModel
{
    public class ScheduledCommand : CommandToExecute
    {
        public int ClientId { get; set; }
        public ScheduleInstruction CommandSchedule { get; set; }

        //Denormalize for easy reading, not mapped
        public string ClientMachineIPv4 { get; set; }

    }
}

