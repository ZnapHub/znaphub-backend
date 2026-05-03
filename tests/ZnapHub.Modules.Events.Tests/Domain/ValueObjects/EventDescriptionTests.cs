using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class EventDescriptionTests
{
    [Fact]
    public void FromString_ReturnsNull_WhenInputIsBlank()
    {
        EventDescription.FromString("   ").ShouldBeNull();
        EventDescription.FromString(null).ShouldBeNull();
    }

    [Fact]
    public void FromString_TrimsWhitespace_WhenInputHasContent()
    {
        var description = EventDescription.FromString("  hello  ");

        description!.ToString().ShouldBe("hello");
    }
}
