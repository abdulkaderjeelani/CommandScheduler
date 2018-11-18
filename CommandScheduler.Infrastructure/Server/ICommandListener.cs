using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Infrastructure.Server.DataContracts;

namespace CommandScheduler.Infrastructure.Server
{
    public interface ICommandListener
    {
        [OperationContract(IsOneWay = true)]
        void OnReceiveCommand(CommandInfo command);

    }
}
