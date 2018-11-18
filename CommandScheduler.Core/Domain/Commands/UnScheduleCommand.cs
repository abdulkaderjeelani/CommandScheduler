using CommandScheduler.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Core.Domain.Commands
{
    public class UnScheduleCommand : Command
    {
        public UnScheduleCommand(int commandId)
        {
            CommandId = commandId;
        }


        public int CommandId { get; private set; }
    }
}
