using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.Core.Domain.ValueObjects
{
    public class CommandResult : ValueObject<CommandResult>
    {
        /// <summary>
        /// True if command executd successfully
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Result of the command exectued
        /// </summary>
        public string Result { get; set; }
    }
}
