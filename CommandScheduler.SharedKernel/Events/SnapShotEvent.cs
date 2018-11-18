using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.SharedKernel;

namespace CommandScheduler.SharedKernel.Events
{
    /// <summary>
    /// Fake event, to instruct the aggregate to save / load it from snapshot
    /// Implement IAggHandleEvent<SnapShotEvent<TAggregate>>
    /// TODO: Implement snapshots in implementation, for every nth version create a snapshotevent and save it, 
    /// while loading back take the recent snapshotevent if available from it, then take the events with 
    /// version > SnapShotEvent.AggVersion
    /// Note: SnapShotEvents are beter stored separately as its not part of Domain
    /// </summary>
    public class SnapShotEvent<TAggregate> : Event
    {

        public SnapShotEvent()
        {

        }

        public SnapShotEvent(TAggregate aggregate)
        {
            Snapshot = aggregate;
        }
        /// <summary>
        /// Snapshot of the aggreate to load from, If we dont use event sourcing.
        /// Snapshot should contain all the state of Aggregate.
        /// We always save the recent state and load from it    
        /// </summary>
        public TAggregate Snapshot { get; set; }

        /// <summary>
        /// Aggregate version in this snapshot
        /// </summary>
        public int AggVersion { get; set; }

        public List<Event> EventsOccuredDuringSnapShot { get; set; }
    }
}
