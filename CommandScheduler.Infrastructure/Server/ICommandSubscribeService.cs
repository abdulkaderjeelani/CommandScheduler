using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CommandScheduler.Infrastructure.Server.DataContracts;
using CommandScheduler.Infrastructure.Server.Response;

namespace CommandScheduler.Infrastructure.Server
{
    [ServiceContract(CallbackContract = typeof(ICommandListener))]
    public interface ICommandSubscribeService
    {
        [OperationContract]
        CommandResponse RegisterClientMachine(ClientMachineRegisterInfo regInfo);

        
    }
}
