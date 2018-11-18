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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CommandService : ICommandBroadcastService, ICommandSubscribeService
    {
        private static Dictionary<string, ICommandListener> clients = new Dictionary<string, ICommandListener>();

        #region Broadcast
        public CommandResponse BroadCastCommand(CommandInfo command)
        {
            CommandResponse response = new CommandResponse();
            try
            {
                string clientKey = command.IPAddressV4;

                Console.WriteLine($"Broad casting command to {clientKey}");

                if (!clients.ContainsKey(clientKey))
                    throw new KeyNotFoundException($"Client with key {clientKey} is not registered.");

                var clientReg = clients.Single(c => c.Key == clientKey);

                var client = (ICommunicationObject)clientReg.Value;

                if (client.State == CommunicationState.Opened)
                    clientReg.Value.OnReceiveCommand(command);

                else
                    throw new InvalidOperationException($"Client with key {clientKey} is not open for connections.");

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //Do logging 
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        #endregion

        #region Subscribe

        private static object locker = new object();

        public CommandResponse RegisterClientMachine(ClientMachineRegisterInfo regInfo)
        {
            CommandResponse response = new CommandResponse();
            try
            {
                lock (locker)
                {
                    string clientKey = regInfo.IPAddressV4;

                    Console.WriteLine($"Registering client with key {clientKey}");

                    if (clients.ContainsKey(clientKey))
                        clients.Remove(clientKey);

                    var listenerProxy = OperationContext.Current.GetCallbackChannel<ICommandListener>();
                    clients.Add(clientKey, listenerProxy);
                    var client = (ICommunicationObject)listenerProxy;
                   
                    client.Closed += (s, e) =>
                    {
                        Console.WriteLine("Closed");
                    };
                    client.Closing += (s, e) =>
                    {
                        Console.WriteLine("Closing");
                    };
                }

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //Do logging 
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        #endregion
    }
}
