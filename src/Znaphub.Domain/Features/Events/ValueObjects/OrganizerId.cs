namespace ZnapHub.Domain.Features.Events.ValueObjects;

public sealed record OrganizerId
{
    private Guid Value { get; }

    private OrganizerId(Guid value) => Value = value;

    public static OrganizerId New() => new(Guid.NewGuid());

    public static OrganizerId FromString(string value) => new(Guid.Parse(value.Trim()));

    public static OrganizerId FromGuid(Guid value) => new(value);

    public static implicit operator Guid(OrganizerId organizerId) => organizerId.Value;
}
