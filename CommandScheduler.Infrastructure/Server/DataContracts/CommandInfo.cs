using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Infrastructure.Server.DataContracts
{
    [DataContract]
    public class CommandInfo
    {
        [DataMember]
        public string CommandText { get; set; }

        [DataMember]
        public string IPAddressV4 { get; set; }
    }
}
