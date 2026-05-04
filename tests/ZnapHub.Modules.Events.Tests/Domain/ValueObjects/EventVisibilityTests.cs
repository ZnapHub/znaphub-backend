using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class EventVisibilityTests
{
    [Fact]
    public void FromBool_True_ReturnsPublic()
    {
        EventVisibility.FromBool(true).ShouldBeOfType<EventVisibility.Public>();
    }

    [Fact]
    public void FromBool_False_ReturnsPrivate()
    {
        EventVisibility.FromBool(false).ShouldBeOfType<EventVisibility.Private>();
    }
}
