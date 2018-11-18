using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.CommandSubscribeService;
using CommandScheduler.Client.Portable.Interface;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class WCFSubscriber : ISubscriber, ICommandSubscribeServiceCallback //as we are using duplex wcf  the listener should implement a callback as defined by server
    {
        /// <summary>
        /// WCF service proxy
        /// </summary>
        CommandSubscribeServiceClient Client { get; set; } = null;

        /// <summary>
        /// Client listener to process the command received, Any command received by the subscriber will 
        /// be dispatched to this listener.
        /// </summary>
        IObserver<ClientCommand> CommandSaga { get; set; } = null;

        public void OnReceiveCommand(CommandInfo command)
        {
            CommandSaga.OnNext(new ClientCommand
            {
                CommandText = command.CommandText
            });
        }

        public IObservable<EventPattern<object>> Subscribe(IObserver<ClientCommand> listener, string ipv4, string machine, string user, out bool isSuccess)
        {
            CommandSaga = listener;

            const string ActiveEndPoint = "NetTcpBinding_ICommandSubscribeService";
            //Register service with duplex channel and Listener as a callback
            var commandContext = new InstanceContext(this);

            //Use the specified endpoint
            Client = new CommandSubscribeServiceClient(commandContext, ActiveEndPoint);
            
            //Invoke register call
            var registrationResponse = Client.RegisterClientMachineAsync(new ClientMachineRegisterInfo
            {
                IPAddressV4 = ipv4
            }).Result;

            if (!registrationResponse.IsSuccess)
                Console.WriteLine($"Registration failed due to {registrationResponse.ErrorMessage}");

            isSuccess = registrationResponse.IsSuccess;

            return Observable.FromEventPattern(Client.ChannelFactory, nameof(Client.ChannelFactory.Closing));
        }

        public void UnSubscribe()
        {
            Client?.Close();
            Client = null;
            if (CommandSaga != null)
            {
                CommandSaga.OnCompleted();
                CommandSaga = null;
            }
        }

    }
}
