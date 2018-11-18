using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Integration.Web;
using CommandScheduler.UserInterface.Web.CodeFiles;

namespace CommandScheduler.UserInterface.Web
{
    public class Global : HttpApplication
    { // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)

        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            _containerProvider = IoC.Setup();

        }
    }
}