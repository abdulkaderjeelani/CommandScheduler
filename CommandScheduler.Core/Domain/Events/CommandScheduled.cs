using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Commands;
using CommandScheduler.Core.Domain.ValueObjects;
using CommandScheduler.SharedKernel;
using CommandScheduler.Core.Domain.Entities;

namespace CommandScheduler.Core.Domain.Events
{
    public class CommandScheduled : Event
    {       
        public CommandScheduled(CommandToExecute commandToExecute, int clientId, ScheduleInstruction scheduleInstruction) : base()
        {
            CommandToExecute = commandToExecute;
            ClientId = clientId;
            ScheduleInstructions = scheduleInstruction;
        }

        public CommandToExecute CommandToExecute { get; private set; }
        public int ClientId { get; private set; }
        public ScheduleInstruction ScheduleInstructions { get; private set; }
        
    }
}
