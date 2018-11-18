using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Utilities.IoC
{
    public struct RegistrationInfo
    {
        public Type Implementation { get; set; }

        public Type Interface { get; set; }

        public object Key { get; set; }

        public Lifecycles Lifecycle { get; set; }
    }
}