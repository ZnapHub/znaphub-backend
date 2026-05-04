using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Domain.ValueObjects;

public class QrCodeStateTests
{
    [Fact]
    public void Active_HoldsExpiryUploadLimitsAndCount()
    {
        var expiresAt = DateTimeOffset.UtcNow.AddDays(7);

        var state = new QrCodeState.Active(expiresAt, MaxUploads: 100, UploadCount: 3);

        state.ExpiresAt.ShouldBe(expiresAt);
        state.MaxUploads.ShouldBe(100);
        state.UploadCount.ShouldBe(3);
    }

    [Fact]
    public void Active_AllowsNullExpiry_ForOpenEndedCodes()
    {
        var state = new QrCodeState.Active(ExpiresAt: null, MaxUploads: 0, UploadCount: 0);

        state.ExpiresAt.ShouldBeNull();
    }

    [Fact]
    public void Expired_RecordsWhenItExpiredAndFinalCounts()
    {
        var expiredAt = DateTimeOffset.UtcNow;

        var state = new QrCodeState.Expired(expiredAt, MaxUploads: 10, UploadCount: 10);

        state.ExpiredAt.ShouldBe(expiredAt);
        state.UploadCount.ShouldBe(10);
    }

    [Fact]
    public void Deactivated_RetainsCountsForAuditability()
    {
        var state = new QrCodeState.Deactivated(MaxUploads: 50, UploadCount: 7);

        state.MaxUploads.ShouldBe(50);
        state.UploadCount.ShouldBe(7);
    }

    [Fact]
    public void Equality_TwoActivesWithSameValuesAreEqual()
    {
        var a = new QrCodeState.Active(null, 10, 0);
        var b = new QrCodeState.Active(null, 10, 0);

        a.ShouldBe(b);
    }
}
