using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.SharedKernel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Persistence.Database
{
    /// <summary>
    /// Additional abstraction to access database, The implementing class may use Any ORM (EF/NH)
    /// </summary>
    public interface ICommandSchedulerDb : IDbActions,
        IDb<Application.DataModel.Client>,
        IDb<Application.DataModel.ScheduledCommand>
    {
        
    }
}
