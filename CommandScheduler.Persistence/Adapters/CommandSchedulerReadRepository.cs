using CommandScheduler.Application.Ports;
using CommandScheduler.Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Persistence.Adapters
{
    public class CommandSchedulerReadRepository : ICommandSchedulerReadRepository
    {
        private readonly ICommandSchedulerDb _database;
        public CommandSchedulerReadRepository(ICommandSchedulerDb database)
        {

        }
    }
}
