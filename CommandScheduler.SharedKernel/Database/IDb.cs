using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Database
{
    public interface IDb<DbEntity>
    {
        IQueryable<DbEntity> Queryable();
        
        void Delete(DbEntity entity);
        void Upsert(DbEntity entity);

    }

    public interface IDbActions
    {
        void SaveChanges();
    }
}
