using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Application.DataModel;
using CommandScheduler.Persistence.Database;
using CommandScheduler.SharedKernel.Database;

namespace CommandScheduler.Persistence.EntityFramework
{
    public sealed partial class CommandSchedulerDataContext : ICommandSchedulerDb
    {
        #region Client

        IQueryable<Client> IDb<Client>.Queryable()
        {
            throw new NotImplementedException();
        }

        public void Upsert(Client entity)
        {
            throw new NotImplementedException();
        }


        public void Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Command

        IQueryable<ScheduledCommand> IDb<ScheduledCommand>.Queryable()
        {
            throw new NotImplementedException();
        }

        public void Upsert(ScheduledCommand entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ScheduledCommand entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }


    }
}
