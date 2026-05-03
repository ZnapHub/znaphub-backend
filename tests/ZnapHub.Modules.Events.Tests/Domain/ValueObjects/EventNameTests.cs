using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class EventNameTests
{
    [Fact]
    public void FromString_TrimsWhitespace_ButPreservesCasing()
    {
        var name = EventName.FromString("  Summer Party  ");

        name.ToString().ShouldBe("Summer Party");
    }
}
