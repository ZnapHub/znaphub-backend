using ZnapHub.Shared.Contracts.Identity;

namespace ZnapHub.Modules.Events.Tests.Fakes;

internal sealed class FakeCurrentUserService : ICurrentUserService
{
    public FakeCurrentUserService(Guid? userId) => UserId = userId;

    public Guid? UserId { get; }
}
