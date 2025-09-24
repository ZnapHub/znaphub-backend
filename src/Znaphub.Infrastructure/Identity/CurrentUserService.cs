using Microsoft.AspNetCore.Http;
using ZnapHub.Application.Abstractions.Identity;

namespace ZnapHub.Infrastructure.Identity;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var sub = user?.FindFirst("sub")?.Value;
            return sub != null ? Guid.Parse(sub) : null;
        }
    }
}
