using Shouldly;
using ZnapHub.Modules.Events.Data;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Data;

public class EventDataMapperTests
{
    [Fact]
    public void DomainToEntityToDomain_RoundTripsAllFields()
    {
        var original = Event.Create(
            OrganizerId.New(),
            EventName.FromString("Summer Party"),
            EventSlug.FromString("summer-party"),
            EventVisibility.FromBool(true),
            EventDescription.FromString("fun")
        );

        var roundTripped = original.ToEntity().ToDomain();

        roundTripped.Id.ShouldBe(original.Id);
        roundTripped.OrganizerId.ShouldBe(original.OrganizerId);
        roundTripped.Name.Value.ShouldBe(original.Name.Value);
        roundTripped.Slug.ShouldBe(original.Slug);
        roundTripped.Description.ShouldBe(original.Description);
        roundTripped.Visibility.ShouldBeOfType<EventVisibility.Public>();
        roundTripped.CreatedAt.ShouldBe(original.CreatedAt);
    }

    [Fact]
    public void EntityToDomain_DoesNotRaiseDomainEvents()
    {
        var entity = new EventEntity
        {
            Id = Guid.CreateVersion7(),
            OrganizerId = Guid.CreateVersion7(),
            Name = "Summer Party",
            Slug = "summer-party",
            Description = null,
            IsPublic = false,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        entity.ToDomain().DomainEvents.ShouldBeEmpty();
    }
}
