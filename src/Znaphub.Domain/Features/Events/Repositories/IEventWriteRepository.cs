using ZnapHub.Domain.Features.Events.Entities;

namespace ZnapHub.Domain.Features.Events.Interfaces;

public interface IEventWriteRepository
{
    Task AddAsync(Event @event);
}
