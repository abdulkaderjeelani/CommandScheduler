using System;
using System.Collections.Generic;
using Autofac;
using CommandScheduler.Utilities.IoC;
using Moq;


namespace CommandScheduler.Tests.Common.Registration
{
    public struct MockRegistrationInfo
    {
        public Type Interface { get; set; }

        public Lifecycles Lifecycle { get; set; }

        public Mock Mocker { get; set; }
    }

    public abstract class MockRegistrator
    {
        private Dictionary<Type, Mock> _mockers = new Dictionary<Type, Mock>();

        protected MockRegistrator()
        {
            Registrations = new List<MockRegistrationInfo>();
        }

        private List<MockRegistrationInfo> Registrations { get; set; }

        public Mock<T> GetMock<T>() where T : class
        {
            return _mockers[typeof(T)] as Mock<T>;
        }

        public void Init(ContainerBuilder builder)
        {
            _mockers = new Dictionary<Type, Mock>();
            InitCore();
            foreach (var mockRegistrationInfo in Registrations)
            {
                builder.RegisterInstance(mockRegistrationInfo.Mocker.Object).As(mockRegistrationInfo.Interface);
            }
        }

        protected abstract void InitCore();

        protected void RegisterAllMocksForLayer(LayerRegistrator layerRegistrator)
        {
            foreach (var registrationInfo in layerRegistrator.Registrations)
            {
                var interfaceToEmulate = registrationInfo.Interface;
                var mocker = Activator.CreateInstance(typeof(Mock<>).MakeGenericType(interfaceToEmulate)) as Mock;
                RegisterMock(mocker, interfaceToEmulate, registrationInfo.Lifecycle);
            }
        }

        protected void RegisterMock<T>(Lifecycles lifecycle = Lifecycles.PerScope) where T : class
        {
            var mocker = new Mock<T>();
            RegisterMock(mocker, lifecycle);
        }

        protected void RegisterMock<T>(Mock<T> mocker, Lifecycles lifecycle = Lifecycles.PerScope) where T : class
        {
            RegisterMock(mocker, typeof(T), lifecycle);
        }

        private void RegisterMock(Mock mocker, Type t, Lifecycles lifecycle = Lifecycles.PerScope)
        {
            _mockers.Add(t, mocker);
            Registrations.Add(new MockRegistrationInfo { Mocker = mocker, Interface = t, Lifecycle = lifecycle });
        }
    }
}