using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.Infrastructure.Server.DataContracts
{
    [DataContract]
    public class ClientMachineRegisterInfo
    {
        [DataMember]        
        public string IPAddressV4 { get; set; }

        [DataMember]
        public string MachineName { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}
