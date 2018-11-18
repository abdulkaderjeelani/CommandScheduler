using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Infrastructure.Scheduler
{

    public class CommandInfo
    {
        public int CommandId { get; set; }
        public int ClientMachineId { get; set; }
    }
}
