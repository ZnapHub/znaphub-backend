using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Domain.ValueObjects;

public class ShortIdTests
{
    [Fact]
    public void FromString_NormalizesByTrimmingAndLowercasing()
    {
        var shortId = ShortId.FromString("  AbC123  ");

        shortId.ToString().ShouldBe("abc123");
    }

    [Fact]
    public void Equality_IgnoresOriginalCasingAndWhitespace()
    {
        ShortId.FromString("AbC123").ShouldBe(ShortId.FromString("  abc123  "));
    }

    [Fact]
    public void ImplicitConversion_ToString_ReturnsNormalizedValue()
    {
        string value = ShortId.FromString("XYZ");

        value.ShouldBe("xyz");
    }
}
