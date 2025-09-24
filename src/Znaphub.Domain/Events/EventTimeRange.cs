namespace ZnapHub.Domain.Events;

public abstract record EventTimeRange
{
    private EventTimeRange() { }

    public sealed record OpenEnded(EventDate StartsAt) : EventTimeRange;

    public sealed record Fixed : EventTimeRange
    {
        public EventDate StartsAt { get; }
        public EventDate EndsAt { get; }

        public Fixed(EventDate startsAt, EventDate endsAt)
        {
            if (endsAt.Value <= startsAt.Value)
                throw new ArgumentException("End date must be after start date.");

            StartsAt = startsAt;
            EndsAt = endsAt;
        }
    }

    public DateTimeOffset Start =>
        this switch
        {
            OpenEnded o => o.StartsAt.Value,
            Fixed f => f.StartsAt.Value,
            _ => throw new InvalidOperationException(),
        };

    public DateTimeOffset? End =>
        this switch
        {
            OpenEnded => null,
            Fixed f => f.EndsAt.Value,
            _ => throw new InvalidOperationException(),
        };
}
