using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Domain.ValueObjects;

public class QrCodeIdTests
{
    [Fact]
    public void New_ProducesVersion7Guid_SoIdsAreTimeOrdered()
    {
        Guid value = QrCodeId.New();

        value.Version.ShouldBe(7);
    }

    [Fact]
    public void New_ProducesUniqueValues()
    {
        Guid a = QrCodeId.New();
        Guid b = QrCodeId.New();

        a.ShouldNotBe(b);
    }

    [Fact]
    public void FromGuid_RoundTripsValue()
    {
        var original = Guid.CreateVersion7();

        Guid value = QrCodeId.FromGuid(original);

        value.ShouldBe(original);
    }
}
