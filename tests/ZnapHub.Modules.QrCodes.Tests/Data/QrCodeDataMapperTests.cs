using Shouldly;
using ZnapHub.Modules.QrCodes.Data;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Data;

public class QrCodeDataMapperTests
{
    [Fact]
    public void DomainToEntityToDomain_RoundTripsActiveState()
    {
        var original = QrCode.Create(
            ShortId.FromString("abc123"),
            Guid.CreateVersion7(),
            maxUploads: 10,
            expiresAt: DateTimeOffset.UtcNow.AddDays(7)
        );

        var roundTripped = original.ToEntity().ToDomain();

        roundTripped.Id.ShouldBe(original.Id);
        roundTripped.ShortId.ShouldBe(original.ShortId);
        roundTripped.EventId.ShouldBe(original.EventId);
        roundTripped.State.ShouldBe(original.State);
    }

    [Fact]
    public void DomainToEntityToDomain_RoundTripsActiveWithNullExpiry()
    {
        var original = QrCode.Create(
            ShortId.FromString("xyz"),
            Guid.CreateVersion7(),
            maxUploads: 0
        );

        var roundTripped = original.ToEntity().ToDomain();

        roundTripped.State.ShouldBeOfType<QrCodeState.Active>();
        roundTripped.State.ShouldBe(original.State);
    }

    [Fact]
    public void DomainToEntityToDomain_RoundTripsExpiredState()
    {
        var qr = QrCode.Create(ShortId.FromString("exp"), Guid.CreateVersion7(), maxUploads: 1);
        qr.IncrementUploadCount();
        qr.State.ShouldBeOfType<QrCodeState.Expired>();

        var roundTripped = qr.ToEntity().ToDomain();

        var expired = roundTripped.State.ShouldBeOfType<QrCodeState.Expired>();
        var originalExpired = (QrCodeState.Expired)qr.State;
        expired.ExpiredAt.ShouldBe(originalExpired.ExpiredAt);
        expired.UploadCount.ShouldBe(originalExpired.UploadCount);
        expired.MaxUploads.ShouldBe(originalExpired.MaxUploads);
    }

    [Fact]
    public void DomainToEntityToDomain_RoundTripsDeactivatedState()
    {
        var qr = QrCode.Create(ShortId.FromString("deact"), Guid.CreateVersion7(), maxUploads: 5);
        qr.IncrementUploadCount();
        qr.Deactivate();

        var roundTripped = qr.ToEntity().ToDomain();

        var deactivated = roundTripped.State.ShouldBeOfType<QrCodeState.Deactivated>();
        deactivated.MaxUploads.ShouldBe(5);
        deactivated.UploadCount.ShouldBe(1);
    }

    [Fact]
    public void EntityToDomain_DoesNotRaiseDomainEvents()
    {
        var entity = new QrCodeEntity
        {
            Id = Guid.CreateVersion7(),
            ShortId = "abc",
            EventId = Guid.CreateVersion7(),
            CreatedAt = DateTimeOffset.UtcNow,
            MaxUploads = 0,
            UploadCount = 0,
            IsActive = true,
        };

        entity.ToDomain().DomainEvents.ShouldBeEmpty();
    }
}
