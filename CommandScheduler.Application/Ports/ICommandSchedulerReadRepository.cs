using CommandScheduler.Core.Domain;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.SharedKernel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommandScheduler.Application.Ports
{
    /// <summary>
    /// Repository for domain writes
    /// "CQS" (unit-level) NOT "CQRS" (db-level and much more)
    /// </summary>
    public interface ICommandSchedulerReadRepository
    {
            
    }
}
