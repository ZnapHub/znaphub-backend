namespace ZnapHub.Modules.Events.Domain.ValueObjects;

public abstract record EventVisibility
{
    private EventVisibility() { }

    public sealed record Public : EventVisibility;

    public sealed record Private : EventVisibility;

    public static EventVisibility FromBool(bool isPublic) =>
        isPublic ? new Public() : new Private();
}
