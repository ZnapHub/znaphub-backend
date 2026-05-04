using Shouldly;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features;

namespace ZnapHub.Modules.Events.Tests.Features;

public class EventDtoMapperTests
{
    [Fact]
    public void ToDto_ProjectsAllFields_AndMapsVisibilityToIsPublic()
    {
        var @event = Event.Create(
            OrganizerId.New(),
            EventName.FromString("Summer Party"),
            EventSlug.FromString("summer-party"),
            EventVisibility.FromBool(true),
            EventDescription.FromString("fun")
        );

        var dto = @event.ToDto();

        dto.Id.ShouldBe((Guid)@event.Id);
        dto.Name.ShouldBe("Summer Party");
        dto.Slug.ShouldBe("summer-party");
        dto.Description.ShouldBe("fun");
        dto.IsPublic.ShouldBeTrue();
        dto.CreatedAt.ShouldBe(@event.CreatedAt);
    }

    [Fact]
    public void ToDto_MapsPrivateVisibilityToFalse()
    {
        var @event = Event.Create(
            OrganizerId.New(),
            EventName.FromString("X"),
            EventSlug.FromString("x"),
            EventVisibility.FromBool(false)
        );

        @event.ToDto().IsPublic.ShouldBeFalse();
    }
}
