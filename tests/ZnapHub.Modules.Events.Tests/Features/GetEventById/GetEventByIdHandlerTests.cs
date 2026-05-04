using Shouldly;
using ZnapHub.Modules.Events.Domain.Entities;
using ZnapHub.Modules.Events.Domain.ValueObjects;
using ZnapHub.Modules.Events.Features.GetEventById;
using ZnapHub.Modules.Events.Tests.Fakes;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.Events.Tests.Features.GetEventById;

public class GetEventByIdHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsDto_WhenEventExists()
    {
        var repo = new FakeEventRepository();
        var @event = Event.Create(
            OrganizerId.New(),
            EventName.FromString("Summer Party"),
            EventSlug.FromString("summer-party"),
            EventVisibility.FromBool(true)
        );
        await repo.AddAsync(@event);

        var handler = new GetEventByIdHandler(repo);

        var result = await handler.HandleAsync(new GetEventByIdQuery(@event.Id));

        result.IsSuccess.ShouldBeTrue();
        result.Value.Id.ShouldBe((Guid)@event.Id);
        result.Value.Slug.ShouldBe("summer-party");
        result.Value.IsPublic.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_ReturnsNotFound_WhenEventDoesNotExist()
    {
        var handler = new GetEventByIdHandler(new FakeEventRepository());

        var result = await handler.HandleAsync(new GetEventByIdQuery(Guid.CreateVersion7()));

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Error.NotFound);
    }
}
