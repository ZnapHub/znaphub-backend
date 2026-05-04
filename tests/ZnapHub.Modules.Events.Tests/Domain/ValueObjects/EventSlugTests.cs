using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class EventSlugTests
{
    [Fact]
    public void FromString_NormalizesByTrimmingAndLowercasing()
    {
        var slug = EventSlug.FromString("  Summer-Party  ");

        slug.ToString().ShouldBe("summer-party");
    }

    [Fact]
    public void Equality_IgnoresOriginalCasingAndWhitespace()
    {
        EventSlug.FromString("Summer-Party").ShouldBe(EventSlug.FromString("  summer-party  "));
    }
}
