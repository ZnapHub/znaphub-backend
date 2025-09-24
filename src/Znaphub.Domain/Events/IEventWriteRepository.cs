namespace ZnapHub.Domain.Events;

public interface IEventWriteRepository
{
    Task AddAsync(Event @event);
}
