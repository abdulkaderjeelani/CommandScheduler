using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Utilities;
using CommandScheduler.SharedKernel.Domain;
using System.Collections.Concurrent;
using System.Threading;
using CommandScheduler.SharedKernel.Exceptions;

namespace CommandScheduler.SharedKernel
{
    /// <summary>
    /// Aggregate Base.
    /// Aggregate can only change its state  in its own EventHandler by implementing <see cref="IAggHandleCommand{TCommand}"/> 
    /// 
    /// </summary>
    public abstract class Aggregate : IEventProvider
    {
        private readonly List<Event> _changes;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public virtual string Key => null;

        protected Aggregate()
        {
            _changes = new List<Event>();
        }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            Version = -1;
            if (history != null)
            {
                foreach (var e in history) ApplyChange(e, false);
                //After loading events set the version of Agg. to last event version (Agg. versionis incremented per event)
                Version = history.Last().AggregateVersion;
            }
        }

        protected virtual void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }


        private void ApplyChange(Event @event, bool isNew)
        {
            dynamic d = this;
            @event.IsNewEvent = isNew;
            d.Handle(Converter.ChangeTo(@event, @event.GetType()));
            if (isNew)
                _changes.Add(@event);

        }


        public virtual void SetId(Guid id)
        {
            Id = id;
        }
    }
    /// <summary>
    /// Marker class for system aggregats
    /// </summary>
    /// <typeparam name="TRootEntity"></typeparam>
    public abstract class Aggregate<TRootEntity> : Aggregate where TRootEntity : Entity<Guid>
    {
        private TRootEntity _aggRoot = null;
        public TRootEntity AggRoot
        {
            get { return _aggRoot; }
            set
            {
                _aggRoot = value;
                Id = AggRoot.Id;
            }

        }
    }

    public abstract class Aggregate<TAggregate, TRootEntity> : Aggregate<TRootEntity>
        where TRootEntity : Entity<Guid>
        where TAggregate : Aggregate
    {
        /// <summary>
        /// Return the Aggregate Object reference (this)
        /// </summary>
        protected abstract TAggregate Self { get; }

        /// <summary>
        /// Load with specifcation instances
        /// If a specification requires Reposiotry / External Access Abstract the spec e.g IUniqueSpecification and supply via DI, then set the needed by calling SetState in interface
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected virtual IEnumerable<ISpecification<TAggregate>> GetInvariants(Event @event) => null;

        protected virtual void EnforceInvariants(IEnumerable<ISpecification<TAggregate>> invariants, InvariantEnforcementStyle enforcemnt = InvariantEnforcementStyle.Collect)
        {
            if (invariants == null || !invariants.Any()) return;

            var exceptions = new ConcurrentBag<InvariantException>();

            Parallel.ForEach(invariants, (iv, loopState) =>
                                         {
                                             dynamic current = this;//check this hack works, if so remove the self property
                                             if (!iv.IsSatisfiedBy(current))
                                             {
                                                     //Any valid string here based on specification
                                                     var ivEx = new InvariantException(iv.GetType().Name);
                                                 if (exceptions != null)
                                                     exceptions.Add(ivEx);
                                                 if (enforcemnt == InvariantEnforcementStyle.FailFast)
                                                     loopState.Stop();
                                             }

                                         });

            if (exceptions.Count > 0)
                throw new InvariantException(exceptions.AsEnumerable());
        }

        protected override void ApplyChange(Event @event)
        {
            EnforceInvariants(GetInvariants(@event));
            base.ApplyChange(@event);
        }
    }


}
