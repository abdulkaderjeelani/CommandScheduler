using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Interface;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client
{
    public class ResultDispatcher : IResultDispatcher
    {
        
        public ResultDispatcher()
        {
            
        }

        public void SendResultToServer(CommandResult result)
        {
            Console.WriteLine("Sending result to server ");
        }
    }
}
