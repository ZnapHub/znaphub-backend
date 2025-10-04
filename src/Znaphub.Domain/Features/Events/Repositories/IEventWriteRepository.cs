using ZnapHub.Domain.Features.Events.Entities;

namespace ZnapHub.Domain.Features.Events.Repositories;

public interface IEventWriteRepository
{
    Task AddAsync(Event @event);
}
