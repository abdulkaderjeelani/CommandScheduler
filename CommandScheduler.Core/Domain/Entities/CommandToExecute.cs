using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.ValueObjects;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.Core.Domain.Entities
{
    public class CommandToExecute : Entity<Guid>
    {
        public CommandToExecute()
        {

        }

        public CommandToExecute(Guid id) : base(id)
        {
        }

        /// <summary>
        /// Text to execute in the shell 
        /// </summary>
        public string CommandText { get; set; }
       
    }
}

