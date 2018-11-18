using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Utilities.IoC;

namespace CommandScheduler.Infrastructure
{
    public class InfrastructureRegistrator : LayerRegistrator
    {
        protected override void Init()
        {
            //Register<class, interface>();
        }
    }
}
