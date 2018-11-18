using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Utilities.IoC
{
    public enum Lifecycles
    {
        Singleton,
        PerScope,
        PerDependency,
        PerRequest
    }
}