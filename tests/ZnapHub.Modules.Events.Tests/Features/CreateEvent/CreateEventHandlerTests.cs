using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features.CreateEvent;
using ZnapHub.Modules.Events.Tests.Fakes;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Tests.Features.CreateEvent;

public class CreateEventHandlerTests
{
    [Fact]
    public async Task Handle_PersistsEventForCurrentUser_WhenAuthenticated()
    {
        var userId = Guid.CreateVersion7();
        var repo = new FakeEventRepository();
        var handler = new CreateEventHandler(new FakeCurrentUserService(userId), repo);

        var result = await handler.HandleAsync(
            new CreateEventCommand("Summer Party", "summer-party", IsPublic: true, "fun")
        );

        result.IsSuccess.ShouldBeTrue();
        var stored = repo.Stored.ShouldHaveSingleItem();
        ((Guid)stored.OrganizerId).ShouldBe(userId);
        stored.Slug.ToString().ShouldBe("summer-party");
        stored.Visibility.ShouldBeOfType<EventVisibility.Public>();
    }

    [Fact]
    public async Task Handle_ReturnsNullValueError_WhenNoCurrentUser()
    {
        var repo = new FakeEventRepository();
        var handler = new CreateEventHandler(new FakeCurrentUserService(null), repo);

        var result = await handler.HandleAsync(new CreateEventCommand("X", "x"));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Error.NullValue);
        repo.Stored.ShouldBeEmpty();
    }
}
