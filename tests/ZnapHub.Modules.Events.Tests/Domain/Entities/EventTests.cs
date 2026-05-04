using Shouldly;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.Events;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.Entities;

public class EventTests
{
    private static readonly OrganizerId Organizer = OrganizerId.New();
    private static readonly EventName Name = EventName.FromString("Summer Party");
    private static readonly EventSlug Slug = EventSlug.FromString("summer-party");
    private static readonly EventVisibility Visibility = EventVisibility.FromBool(true);

    [Fact]
    public void Create_RaisesEventCreated_WithMatchingIdAndOrganizer()
    {
        var @event = Event.Create(Organizer, Name, Slug, Visibility);

        var raised = @event.DomainEvents.ShouldHaveSingleItem().ShouldBeOfType<EventCreated>();
        raised.EventId.ShouldBe(@event.Id);
        raised.OrganizerId.ShouldBe(Organizer);
    }

    [Fact]
    public void Create_DefaultsDescriptionToEmpty_WhenNotProvided()
    {
        var @event = Event.Create(Organizer, Name, Slug, Visibility, description: null);

        @event.Description.ShouldBe(EventDescription.Empty);
    }

    [Fact]
    public void Rehydrate_DoesNotRaiseDomainEvents()
    {
        var @event = Event.Rehydrate(
            EventId.New(),
            Organizer,
            Name,
            Slug,
            Visibility,
            createdAt: DateTimeOffset.UtcNow.AddDays(-1)
        );

        @event.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void UpdateVisibility_ChangesVisibilityAndStampsUpdatedAt()
    {
        var @event = Event.Create(Organizer, Name, Slug, EventVisibility.FromBool(true));
        @event.UpdatedAt.ShouldBeNull();

        @event.UpdateVisibility(EventVisibility.FromBool(false));

        @event.Visibility.ShouldBeOfType<EventVisibility.Private>();
        @event.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public void UpdateDetails_ChangesNameAndDescriptionAndStampsUpdatedAt()
    {
        var @event = Event.Create(Organizer, Name, Slug, Visibility);

        var newName = EventName.FromString("Autumn Gala");
        var newDescription = EventDescription.FromString("Annual fundraiser")!;
        @event.UpdateDetails(newName, newDescription);

        @event.Name.ShouldBe(newName);
        @event.Description.ShouldBe(newDescription);
        @event.UpdatedAt.ShouldNotBeNull();
    }
}
