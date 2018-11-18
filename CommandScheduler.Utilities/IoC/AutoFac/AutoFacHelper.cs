using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;

namespace CommandScheduler.Utilities.IoC.AutoFac
{
    public static class AutoFacHelper
    {
        public static void Register<T>(ContainerBuilder builder)
            where T : LayerRegistrator, new()
        {
            LayerRegistrator moduleRegistrator = LayerRegistrator.Create<T>();
            List<RegistrationInfo> registrationInfos = moduleRegistrator.Registrations;
            foreach (var registrationInfo in registrationInfos)
            {
                IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> tempReg;

                if (registrationInfo.Interface == null)
                {
                    tempReg = builder.RegisterType(registrationInfo.Implementation)
                        .OnActivated(args => AutowiringNonPublicPropertyInjector.InjectProperties(args.Context, args.Instance, true));
                        

                }
                else
                {
                    if (registrationInfo.Key != null)
                    {
                        tempReg = builder.RegisterType(registrationInfo.Implementation)
                            .Keyed(registrationInfo.Key, registrationInfo.Interface);
                    }
                    else
                    {
                        tempReg = builder.RegisterType(registrationInfo.Implementation).As(registrationInfo.Interface);
                    }
                }

                tempReg.PropertiesAutowired();

                switch (registrationInfo.Lifecycle)
                {
                    case Lifecycles.PerDependency:
                        tempReg.InstancePerDependency();
                        break;
                    case Lifecycles.PerScope:
                        tempReg.InstancePerLifetimeScope();
                        break;
                    case Lifecycles.Singleton:
                        tempReg.SingleInstance();
                        break;
                    case Lifecycles.PerRequest:
                    default:
                        tempReg.InstancePerRequest();
                        break;
                }
            }
        }
    }
}