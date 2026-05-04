using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.Repositories;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Fakes;

internal sealed class FakeEventRepository : IEventRepository
{
    private readonly List<Event> _store = [];

    public IReadOnlyList<Event> Stored => _store;

    public Task AddAsync(Event @event, CancellationToken ct = default)
    {
        _store.Add(@event);
        return Task.CompletedTask;
    }

    public Task<Event?> GetByIdAsync(EventId id, CancellationToken ct = default) =>
        Task.FromResult(_store.FirstOrDefault(e => e.Id == id));

    public Task<IReadOnlyList<Event>> GetByOrganizerIdAsync(
        OrganizerId id,
        CancellationToken ct = default
    ) => Task.FromResult<IReadOnlyList<Event>>(_store.Where(e => e.OrganizerId == id).ToList());
}
