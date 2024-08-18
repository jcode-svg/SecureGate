using MediatR;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.Events;
using SecureGate.Domain.RepositoryContracts;

namespace SecureGate.Application.EventHandlers
{
    public class AccessAttemptLoggedEventHandler : INotificationHandler<AccessAttemptLoggedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;

        public AccessAttemptLoggedEventHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }

        public async Task Handle(AccessAttemptLoggedEvent notification, CancellationToken cancellationToken)
        {
            var eventLog = EventLog.CreateEventLog(notification.EmployeeId, notification.DoorId);
            eventLog.UpdateAccessStatus(notification.IsAuthorized);
            await _eventLogRepository.AddEventLogAsync(eventLog);
            await _eventLogRepository.SaveChangesAsync();
        }
    }
}
