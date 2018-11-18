using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client.Portable.Interface
{
    public interface ISubscriber 
    {
        /// <summary>
        /// Subscribe to the server using WCF / Message Queing
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="ipv4"></param>
        /// <param name="machine"></param>
        /// <param name="user"></param>
        /// <param name="isSuccess"></param>
        /// <returns>
        ///  Return the closing state as observable to retry after closing. 
        ///  Observer will be notified when the underlying client connection is closed.
        /// </returns>
        IObservable<EventPattern<object>> Subscribe(IObserver<ClientCommand> listener, string ipv4, string machine, string user, out bool isSuccess);
        /// <summary>
        /// Unsubscribe from the server, underlying connection will be closed
        /// </summary>        
        void UnSubscribe();
    }
}
