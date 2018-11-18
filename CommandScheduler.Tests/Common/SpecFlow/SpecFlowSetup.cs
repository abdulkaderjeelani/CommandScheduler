using TechTalk.SpecFlow;

namespace CommandScheduler.Tests.Common.SpecFlow
{
    [Binding]
    public class SpecFlowSetup
    {
        [BeforeScenario]
        public static void BeforeTestRun()
        {
            // probably not the most performant solution. Maybe we should reuse container, just clear all mocks and create new scope
            Bootstrapper.Init();
        }

        [AfterScenario]
        public static void Cleanup()
        {
            Bootstrapper.Cleanup();
        }
    }
}