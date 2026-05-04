using Shouldly;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features.GetEventsByOrganizer;
using ZnapHub.Modules.Events.Tests.Fakes;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Tests.Features.GetEventsByOrganizer;

public class GetEventsByOrganizerHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsOnlyEventsBelongingToCurrentUser()
    {
        var userId = Guid.CreateVersion7();
        var organizer = OrganizerId.FromGuid(userId);
        var otherOrganizer = OrganizerId.New();

        var mine = Event.Create(
            organizer,
            EventName.FromString("Mine"),
            EventSlug.FromString("mine"),
            EventVisibility.FromBool(false)
        );
        var theirs = Event.Create(
            otherOrganizer,
            EventName.FromString("Theirs"),
            EventSlug.FromString("theirs"),
            EventVisibility.FromBool(false)
        );

        var repo = new FakeEventRepository();
        await repo.AddAsync(mine);
        await repo.AddAsync(theirs);

        var handler = new GetEventsByOrganizerHandler(new FakeCurrentUserService(userId), repo);

        var result = await handler.HandleAsync(new GetEventsByOrganizerQuery());

        result.IsSuccess.ShouldBeTrue();
        var dto = result.Value.ShouldHaveSingleItem();
        dto.Id.ShouldBe((Guid)mine.Id);
    }

    [Fact]
    public async Task Handle_ReturnsNullValueError_WhenNoCurrentUser()
    {
        var handler = new GetEventsByOrganizerHandler(
            new FakeCurrentUserService(null),
            new FakeEventRepository()
        );

        var result = await handler.HandleAsync(new GetEventsByOrganizerQuery());

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Error.NullValue);
    }
}
