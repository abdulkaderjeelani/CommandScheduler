using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Commands;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.Core.Domain.Events;
using CommandScheduler.Core.Domain.ValueObjects;
using CommandScheduler.SharedKernel;
using CommandScheduler.Utilities;
using CommandScheduler.SharedKernel.Events;

namespace CommandScheduler.Core.Domain
{

    /// <summary>
    /// Agggregate with single root <see cref="CommandToExecute"/> 
    /// </summary>
    public class CommandAggregate : Aggregate<CommandAggregate, CommandToExecute>,       
        IAggHandleCommand<ScheduleCommand>,
        IAggHandleCommand<UnScheduleCommand>,
        IAggHandleEvent<CommandScheduled>
    {

        public CommandAggregate()
        {
            
        }

        protected override CommandAggregate Self => this;

        /// <summary>
        /// 1 cmd may have multiple schedules, 
        /// </summary>
        public List<ScheduleInstruction> CommandSchedules { get; set; }

        /// <summary>
        /// 1 cmd may run on many machines
        /// </summary>
        public List<int> Clients { get; set; }

        public void Handle(SnapShotEvent<CommandAggregate> e)
        {
            AggRoot = e.Snapshot.AggRoot;
            CommandSchedules = e.Snapshot.CommandSchedules;
            Clients = e.Snapshot.Clients;

        }


        public void Execute(ScheduleCommand c)
        {
            Guard.ForNull(c);

            if (c.ClientIds.Count > 1 || c.ScheduleInstructions.Count > 1)
                throw new NotSupportedException("Sorry, multiple clients or instructions is not supported now.");


            ApplyChange(new CommandScheduled(c.CommandToExecute, c.ClientIds.First(), c.ScheduleInstructions.First()));

        }

        public void Handle(CommandScheduled e)
        {
            AggRoot = e.CommandToExecute;
            CommandSchedules = new List<ScheduleInstruction> { e.ScheduleInstructions };
            Clients = new List<int> { e.ClientId };


        }

        public void Execute(UnScheduleCommand c)
        {
            if (AggRoot == null)
                throw new InvalidOperationException("Command to un-schedule is not specified");

        }

    }
}
