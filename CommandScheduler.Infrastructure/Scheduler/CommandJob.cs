using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;

namespace CommandScheduler.Infrastructure.Scheduler
{
    public class CommandJob
    {
        public const string CommandDatakey = "COMMANDDATA";
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string commandJobInfoJson = dataMap.GetString(CommandDatakey);
            var commandJobInfo = JsonConvert.DeserializeObject<CommandInfo>(commandJobInfoJson);
            
        }
    }
}
