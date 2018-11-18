using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Presentation
{
    public class SessionState
    {
        public ISessionProvider Provider { get; set; }

        public AppUser AppUser
        {
            get { return Get<AppUser>(); }
            set { Set<AppUser>(value); }
        }


        private T Get<T>()
        {
            return (T)Provider[nameof(T)];
        }

        private void Set<T>(T val)
        {
            Provider[nameof(T)] = val;
        }

    }
}
