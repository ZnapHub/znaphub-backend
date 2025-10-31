namespace ZnapHub.Domain.Interfaces;

public interface IEntity<out TId>
{
    TId Id { get; }
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset? UpdatedAt { get; }
}
