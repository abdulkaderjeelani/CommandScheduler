using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Core.Domain.Entities;
using CommandScheduler.Core.Domain.ValueObjects;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.Core.Domain.Commands
{
    public class ScheduleCommand : Command
    {
        private List<int> _clientIds;
        private List<ScheduleInstruction> _scheduleInstructions;
       
        /// <summary>
        /// Schedules the provided command for All Clients provided for All Schedules
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandToExecute"></param>
        /// <param name="clientIds"></param>
        /// <param name="scheduleInstructions"></param>
        /// <param name="aggId"></param>
        /// <param name="version"></param>
        public ScheduleCommand(Guid id, CommandToExecute commandToExecute, List<int> clientIds, List<ScheduleInstruction> scheduleInstructions, Guid aggId, int version) : base(aggId, version)
        {
            if ((clientIds == null || clientIds.Count == 0) || (scheduleInstructions == null || scheduleInstructions.Count == 0))
                throw new ArgumentException("Atlease 1 client id and schedule instruction is required for this commad.");

            CommandToExecute = commandToExecute;
            _clientIds = clientIds;
            _scheduleInstructions = scheduleInstructions;
            Id = id;
        }

        /// <summary>
        /// 1 command may have multiple ids
        /// </summary>
        public IReadOnlyCollection<int> ClientIds =>  _clientIds.AsReadOnly();
        public IReadOnlyCollection<ScheduleInstruction> ScheduleInstructions => _scheduleInstructions.AsReadOnly(); 
        public CommandToExecute CommandToExecute { get; private set; }


    }
}
