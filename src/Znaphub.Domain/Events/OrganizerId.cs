namespace ZnapHub.Domain.Events;

public sealed record OrganizerId(Guid Value)
{
    public static OrganizerId New() => new(Guid.NewGuid());

    public static OrganizerId FromString(string value) => new(Guid.Parse(value.Trim()));

    public static OrganizerId FromGuid(Guid value) => new(value);

    public static implicit operator string(OrganizerId organizerId) => organizerId.ToString();
}
