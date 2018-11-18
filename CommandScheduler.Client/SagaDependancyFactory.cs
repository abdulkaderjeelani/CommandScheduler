using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Interface;

namespace CommandScheduler.Client
{
    /// <summary>
    /// Factory to provide necessary instances for the saga and registrator (Use DI if needed)
    /// </summary>
    public class SagaDependancyFactory : ISagaDependancyFactory
    {
        public ICommandExecutor GetCommandExecutor()
        {
            return new CommandExecutor();
        }

        public IResultDispatcher GetResultDispatcher()
        {            
            return new ResultDispatcher();
        }
    }

    
}
