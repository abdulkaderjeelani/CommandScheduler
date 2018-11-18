using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Interface;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client
{
    public class CommandExecutor : ICommandExecutor
    {
        public Task<CommandResult> Execute(ClientCommand command)
        {
            Console.WriteLine("Executing command");
            return Task.Run<CommandResult>(() => new CommandResult());
        }
    }
}
