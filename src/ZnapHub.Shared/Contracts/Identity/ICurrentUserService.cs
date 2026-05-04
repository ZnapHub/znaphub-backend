namespace ZnapHub.Shared.Contracts.Identity;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
