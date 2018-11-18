using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommandScheduler.Presentation.ViewModels
{
    public class ViewModelBase
    {
        public bool IsAuditable()
        {
            return this is IAuditable;
        }
    }
    public class ClientMachinViewModel : ViewModelBase
    {

    }


    public class ScheduledCommandViewModel : ViewModelBase
    {

    }
}
