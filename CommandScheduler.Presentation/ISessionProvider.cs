using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Presentation
{
    public interface ISessionProvider
    {
        object this[string index]
        {
            get;
            set;
        }
    }
}
