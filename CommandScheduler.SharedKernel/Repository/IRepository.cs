using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel.Repository
{   
    public interface IAggregateRepository<TAggregate> where TAggregate : Aggregate
    {
        void Save(Guid id, IEnumerable<Event> events, int expectedVersion);
        TAggregate Get(Guid id);
    }
}
