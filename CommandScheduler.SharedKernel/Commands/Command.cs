using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandScheduler.SharedKernel
{   
    [Serializable]
    public abstract class Command : ICommand
    {
        public Guid Id { get; protected set; }
        /// <summary>
        /// Aggregate version for this command, In CQRS if read model is saved else where, then
        /// The aggregate version must also be saved with it.
        /// </summary>
        public int AggregateVersion { get; protected set; }
        

        public Command(Guid id, int aggVersion)
        {
            Id = id;
            AggregateVersion = aggVersion;
        }


        public int EntityId { get; protected set; }

        /// <summary>
        /// If we are not using Agg. 
        /// </summary>
        public int EntityVersion { get; protected set; }

        public Command(int entityId, int entityVersion)
        {
            EntityId = entityId;
            EntityVersion = entityVersion;
        }

        public Command()
        {

        }
    }
}
