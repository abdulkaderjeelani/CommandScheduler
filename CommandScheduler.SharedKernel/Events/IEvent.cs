using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandScheduler.SharedKernel
{
    public interface IEvent
    {
        Guid Id { get; }
    }

}
