using Autofac;
using Autofac.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using CommandScheduler.Infrastructure;
using CommandScheduler.Utilities.IoC.AutoFac;

namespace CommandScheduler.UserInterface.Web.CodeFiles
{
    public static class IoC
    {
        private static ContainerBuilder Builder { get; set; }

        public static IContainerProvider Setup()
        {
            // Build up your application container and register your dependencies.
            Builder = new ContainerBuilder();
            RegisterDependancies();
            var containerProvider = new ContainerProvider(Builder.Build());
            return containerProvider;

        }

        private static void RegisterDependancies()
        {
            AutoFacHelper.Register<InfrastructureRegistrator>(Builder);
        }

    }

}
