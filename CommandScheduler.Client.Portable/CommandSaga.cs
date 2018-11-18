using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable.Interface;
using CommandScheduler.Client.Portable.Model;

namespace CommandScheduler.Client.Portable
{
    public class CommandSaga : IObserver<ClientCommand>
    {
        public Guid ClientId { get; }
        ISagaDependancyFactory SagaDependancyFactory { get; set; } = null;
        ConcurrentBag<dynamic> CommandTasks { get; set; } = new ConcurrentBag<dynamic>();
        Timer TaskMonitor { get; set; } = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sagaDependancyFactory">Factory sto supply the depenancies required by the saga <see cref="CommandSaga"/> </param>
        public CommandSaga(ISagaDependancyFactory sagaDependancyFactory)
        {
            this.SagaDependancyFactory = sagaDependancyFactory;
            this.ClientId = ClientId;
            const int IntervalMins = 5;
            TaskMonitor = new Timer(CleanupTasks, null, IntervalMins * 60 * 1000, IntervalMins * 60 * 1000);
        }


        public void OnCompleted()
        {
            TaskMonitor.Dispose();
            CleanupTasks(null);
            if (CommandTasks.Count > 0)
                Task.WaitAll(CommandTasks.Select(t => (Task)t.Task).ToArray());
        }

        public void OnError(Exception error)
        {
        }

        private static readonly object TaskLocker = new object();
        public void OnNext(ClientCommand value)
        {
            lock (TaskLocker)
            {
                CommandTasks.Add(
                    new
                    {
                        Command = value,
                        Task = Task.Run(() =>
                             {
                                 SagaDependancyFactory.GetCommandExecutor().Execute(value).ContinueWith(result =>
                                 SagaDependancyFactory.GetResultDispatcher().SendResultToServer(result.Result));
                                 //throw new NotImplementedException();

                             })
                    });
            }

        }

        private void CleanupTasks(object state)
        {
            Parallel.ForEach(CommandTasks, t =>
            {
                dynamic ot = null;
                if (CommandTasks.TryTake(out ot))
                {
                    var tsk = (Task)ot.Task;
                    var cmd = (ClientCommand)ot.Command;

                    if (ot.Exception != null)
                    {
                        //Do logging                       
                    }
                }
            });
        }
    }
}
