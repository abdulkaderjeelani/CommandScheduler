using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Client.Portable;

namespace CommandScheduler.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Service();

            s.Start();
            Console.ReadLine();

            s.Stop();
            Console.ReadLine();
        }

      
    }

    class Service
    {
        static readonly Registrator Registrator = null;

        /// <summary>
        /// Type initializer to instantiate registrator with 
        /// 1.a subscriber for server connection (implemented with wcf / message queue or any)
        /// 2.saga for processing the commands received by the listener        
        /// </summary>
        static Service()
        {                        
            Registrator = new Registrator(new WCFSubscriber(), new CommandSaga(new SagaDependancyFactory()));
        }

        public void Start()
        {
            bool isRegistered = Registrator.Register((s, e) => TryRegistering());
            if (!isRegistered) TryRegistering();            
        }

        public void Stop()
        {
            Registrator.UnRegister();
        }

        private void TryRegistering()
        {
            const int maxTries = 10;
            int tries = 0;
            while (!Registrator.Register((s, e) => TryRegistering()))
            {
                tries++;
                System.Threading.Thread.Sleep(5000);

                if (tries > maxTries)
                {
                    Console.WriteLine($"Registration could not succceed after {maxTries} attempts");
                    break;
                }

            }
        }
    }



}
