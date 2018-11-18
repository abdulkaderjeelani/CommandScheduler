using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommandScheduler.Presentation;

namespace CommandScheduler.UserInterface.Web.CodeFiles
{
    public class AspNetSessionProvider : ISessionProvider
    {
        public object this[string key]
        {
            get { return HttpContext.Current.Session[key]; }
            set { HttpContext.Current.Session[key] = value; }
            
        }
    }
}