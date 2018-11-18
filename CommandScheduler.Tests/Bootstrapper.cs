using System;
using Autofac;
using CommandScheduler.Infrastructure;
using CommandScheduler.Tests.Registration;
using CommandScheduler.Utilities.IoC;
using CommandScheduler.Utilities.IoC.AutoFac;
using CommandScheduler.Application;

namespace CommandScheduler.Tests
{
    public static class Bootstrapper
    {
        public static InfrastructureMockRegistrator InfrastructureMockRegistrator = new InfrastructureMockRegistrator();
        
        private static IContainer container;

        private static ILifetimeScope mainScope;

        public static ContainerBuilder Builder { get; set; }

        public static ILifetimeScope MainScope
        {
            get
            {
                return mainScope;
            }

            set
            {
                mainScope = value;
            }
        }

        private static IContainer Container
        {
            get
            {
                return container;
            }

            set
            {
                container = value;
            }
        }

        public static void Cleanup()
        {
            container = null;
            mainScope = null;
            InfrastructureMockRegistrator = null;            
        }

        public static void Init()
        {
            InfrastructureMockRegistrator = new InfrastructureMockRegistrator();            
            Builder = new ContainerBuilder();
            RegisterTypes();
            InitContainer();
        }

        private static void InitContainer()
        {
            if (container == null)
            {
                container = Builder.Build();
                MainScope = container.BeginLifetimeScope(Guid.NewGuid().ToString());
                Start();
            }
        }

        private static void RegisterTypes()
        {
            //Actuals
            AutoFacHelper.Register<ApplicationRegistrator>(Builder);
            
            //Mocks
            InfrastructureMockRegistrator.Init(Builder);           
        }

        private static void Start()
        {
        }
    }
}
