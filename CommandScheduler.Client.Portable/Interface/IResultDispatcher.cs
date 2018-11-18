using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client.Portable.Interface
{
    public interface IResultDispatcher
    {
        void SendResultToServer(CommandResult result);
    }
}
