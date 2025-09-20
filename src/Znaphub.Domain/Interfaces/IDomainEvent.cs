namespace Znaphub.Domainz.Interfaces;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}
