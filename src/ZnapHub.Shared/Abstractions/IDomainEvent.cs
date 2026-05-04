namespace ZnapHub.Shared.Abstractions;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
