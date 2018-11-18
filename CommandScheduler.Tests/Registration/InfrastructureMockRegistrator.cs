
using CommandScheduler.Infrastructure;
using CommandScheduler.Tests.Common.Registration;
using CommandScheduler.Utilities.IoC;

namespace CommandScheduler.Tests.Registration
{
    public class InfrastructureMockRegistrator : MockRegistrator
    {
        protected override void InitCore()
        {
            RegisterAllMocksForLayer(LayerRegistrator.Create<InfrastructureRegistrator>());
        }
    }
}