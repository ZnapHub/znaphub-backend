using Shouldly;
using ZnapHub.Modules.Events.Domain.ValueObjects;

namespace ZnapHub.Modules.Events.Tests.Domain.ValueObjects;

public class OrganizerIdTests
{
    [Fact]
    public void New_ProducesVersion7Guid_SoIdsAreTimeOrdered()
    {
        Guid value = OrganizerId.New();

        value.Version.ShouldBe(7);
    }
}
