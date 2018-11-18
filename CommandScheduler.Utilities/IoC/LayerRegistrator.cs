using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Utilities.IoC
{
    public abstract class LayerRegistrator
    {
        protected LayerRegistrator()
        {
            Registrations = new List<RegistrationInfo>();
        }

        public List<RegistrationInfo> Registrations { get; private set; }

        public static LayerRegistrator Create<T>() where T : LayerRegistrator, new()
        {
            var ret = new T();
            ret.Init();
            return ret;
        }

        protected abstract void Init();

        protected void Register<Class, Interface>(Lifecycles lifecycle = Lifecycles.PerScope) where Class : Interface
        {
            Registrations.Add(
                new RegistrationInfo
                    {
                        Implementation = typeof(Class), 
                        Interface = typeof(Interface), 
                        Lifecycle = lifecycle
                    });
        }

        protected void Register<Class, Interface>(object key, Lifecycles lifecycle = Lifecycles.PerDependency)
        {
            Registrations.Add(
                new RegistrationInfo
                    {
                        Implementation = typeof(Class), 
                        Interface = typeof(Interface), 
                        Lifecycle = lifecycle, 
                        Key = key
                    });
        }

        protected void Register<Class>(Lifecycles lifecycle = Lifecycles.PerDependency)
        {
            Registrations.Add(
                new RegistrationInfo
                {
                    Implementation = typeof(Class),
                    Interface = null,
                    Lifecycle = lifecycle,                    
                });
        }
    }
}