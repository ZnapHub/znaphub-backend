namespace ZnapHub.Domain.Events.ValueObjects;

public sealed record OrganizerId
{
    public Guid Value { get; }

    private OrganizerId(Guid value) => Value = value;

    public static OrganizerId New() => new(Guid.NewGuid());

    public static OrganizerId FromString(string value) => new(Guid.Parse(value.Trim()));

    public static OrganizerId FromGuid(Guid value) => new(value);

    public static implicit operator string(OrganizerId organizerId) => organizerId.ToString();
}
