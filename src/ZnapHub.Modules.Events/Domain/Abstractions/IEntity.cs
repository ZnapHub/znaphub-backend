namespace ZnapHub.Modules.Events.Domain.Abstractions;

public interface IEntity<out TId>
{
    TId Id { get; }
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset? UpdatedAt { get; }
}
