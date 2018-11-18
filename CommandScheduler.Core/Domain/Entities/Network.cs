using CommandScheduler.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Core.Domain.Entities
{
    public class Network : Entity<Guid>
    {
        public Network(Guid id) : base(id)
        {

        }

    }
}
