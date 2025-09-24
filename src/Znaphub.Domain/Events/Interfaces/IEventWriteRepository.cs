using ZnapHub.Domain.Events.Entities;

namespace ZnapHub.Domain.Events.Interfaces;

public interface IEventWriteRepository
{
    Task AddAsync(Event @event);
}
