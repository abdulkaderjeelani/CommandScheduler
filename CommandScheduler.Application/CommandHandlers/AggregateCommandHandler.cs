using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandScheduler.Application.Ports;
using CommandScheduler.SharedKernel;
using CommandScheduler.SharedKernel.Events;
using CommandScheduler.SharedKernel.Repository;

namespace CommandScheduler.Application.CommandHandlers
{
    public class AggregateCommandHandler<TCommand, TAggregate> : ICommandHandler<TCommand>
        where TCommand : Command
        where TAggregate : Aggregate, new()
    {
        private readonly IAggregateRepository<TAggregate> _aggRepository;
        private readonly IEventPublisher _eventPublisher;

        public AggregateCommandHandler(IAggregateRepository<TAggregate> aggRepository, IEventPublisher eventPublisher)
        {
            _aggRepository = aggRepository;
            _eventPublisher = eventPublisher;
        }

        public void Execute(TCommand command)
        {
            var agg = _aggRepository.Get(command.Id);
            (agg as IAggHandleCommand<TCommand>).Execute(command);
            var events = agg.GetUncommittedChanges();
            _aggRepository.Save(agg.Id, events, command.AggregateVersion);
            _eventPublisher.PublishEvents(events);

        }
    }
}
