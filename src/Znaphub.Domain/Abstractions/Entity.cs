using ZnapHub.Domain.Interfaces;

namespace ZnapHub.Domain.Abstractions;

public abstract class Entity<TId> : IEntity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(TId id)
    {
        Id = id;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Entity() { }

    public TId Id { get; protected init; } = default!;
    public DateTimeOffset CreatedAt { get; protected init; }
    public DateTimeOffset? UpdatedAt { get; protected set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void Clear()
    {
        _domainEvents.Clear();
    }

    protected void UpdateTimestamp()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
