using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class EventIdTests
{
    [Fact]
    public void New_ProducesVersion7Guid_SoIdsAreTimeOrdered()
    {
        Guid value = EventId.New();

        value.Version.ShouldBe(7);
    }

    [Fact]
    public void New_ProducesUniqueValues()
    {
        Guid a = EventId.New();
        Guid b = EventId.New();

        a.ShouldNotBe(b);
    }
}
