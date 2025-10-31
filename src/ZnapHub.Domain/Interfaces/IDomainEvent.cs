namespace ZnapHub.Domain.Interfaces;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
