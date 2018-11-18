using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Presentation
{
    /// <summary>
    /// Service for the presenter to perform crud operations, The type implementing can 
    /// 1. Call the repository when following a simple CRUD pattern
    /// 2. Create a command and pass it to the application layer when using rich domain model (DDD)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICRUDService<T> where T : class
    {
        T Get(int id);               
        List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> where = null);
        T Insert(T toInsert);
        T Update(T toUpdate);
        T Delete(T toDelete);                
    }
}
