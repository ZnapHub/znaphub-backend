using Shouldly;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.Errors;
using ZnapHub.Modules.QrCodes.Domain.Events;
using ZnapHub.Modules.QrCodes.Domain.ValueObjects;

namespace ZnapHub.Modules.QrCodes.Tests.Domain.Entities;

public class QrCodeTests
{
    private static readonly ShortId Short = ShortId.FromString("abc123");
    private static readonly Guid EventId = Guid.CreateVersion7();

    [Fact]
    public void Create_StartsActive_WithProvidedLimits()
    {
        var expiresAt = DateTimeOffset.UtcNow.AddDays(1);

        var qr = QrCode.Create(Short, EventId, maxUploads: 5, expiresAt: expiresAt);

        var active = qr.State.ShouldBeOfType<QrCodeState.Active>();
        active.MaxUploads.ShouldBe(5);
        active.UploadCount.ShouldBe(0);
        active.ExpiresAt.ShouldBe(expiresAt);
    }

    [Fact]
    public void Create_RaisesQrCodeGenerated_WithMatchingIds()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);

        var raised = qr.DomainEvents.ShouldHaveSingleItem().ShouldBeOfType<QrCodeGenerated>();
        raised.QrCodeId.ShouldBe(qr.Id);
        raised.EventId.ShouldBe(EventId);
    }

    [Fact]
    public void Rehydrate_DoesNotRaiseDomainEvents()
    {
        var qr = QrCode.Rehydrate(
            QrCodeId.New(),
            Short,
            EventId,
            new QrCodeState.Active(null, 10, 3),
            createdAt: DateTimeOffset.UtcNow.AddDays(-2)
        );

        qr.DomainEvents.ShouldBeEmpty();
    }

    [Fact]
    public void IncrementUploadCount_WhileActive_BumpsCount()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 10);

        var result = qr.IncrementUploadCount();

        result.IsSuccess.ShouldBeTrue();
        var active = qr.State.ShouldBeOfType<QrCodeState.Active>();
        active.UploadCount.ShouldBe(1);
        qr.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public void IncrementUploadCount_WhenReachingMaxUploads_TransitionsToExpired()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 2);
        qr.IncrementUploadCount();

        var result = qr.IncrementUploadCount();

        result.IsSuccess.ShouldBeTrue();
        var expired = qr.State.ShouldBeOfType<QrCodeState.Expired>();
        expired.UploadCount.ShouldBe(2);
        qr.DomainEvents.OfType<QrCodeExpired>().ShouldHaveSingleItem();
    }

    [Fact]
    public void IncrementUploadCount_WithUnlimitedMaxUploads_NeverExpires()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);

        for (var i = 0; i < 50; i++)
            qr.IncrementUploadCount();

        var active = qr.State.ShouldBeOfType<QrCodeState.Active>();
        active.UploadCount.ShouldBe(50);
    }

    [Fact]
    public void IncrementUploadCount_WhenNotActive_FailsWithCannotIncrementInactive()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);
        qr.Deactivate();

        var result = qr.IncrementUploadCount();

        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(QrCodeErrors.CannotIncrementInactive);
    }

    [Fact]
    public void Deactivate_FromActive_TransitionsAndRaisesEvent()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);

        qr.Deactivate();

        qr.State.ShouldBeOfType<QrCodeState.Deactivated>();
        qr.DomainEvents.OfType<QrCodeDeactivated>().ShouldHaveSingleItem();
        qr.UpdatedAt.ShouldNotBeNull();
    }

    [Fact]
    public void Deactivate_WhenAlreadyInactive_IsNoOp()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);
        qr.Deactivate();
        var firstUpdated = qr.UpdatedAt;

        qr.Deactivate();

        qr.DomainEvents.OfType<QrCodeDeactivated>().Count().ShouldBe(1);
        qr.UpdatedAt.ShouldBe(firstUpdated);
    }

    [Fact]
    public void CanUpload_True_WhileActive_WithRoomAndNotExpired()
    {
        var qr = QrCode.Create(
            Short,
            EventId,
            maxUploads: 10,
            expiresAt: DateTimeOffset.UtcNow.AddHours(1)
        );

        qr.CanUpload().ShouldBeTrue();
    }

    [Fact]
    public void CanUpload_False_WhenExpiresAtInPast()
    {
        var qr = QrCode.Rehydrate(
            QrCodeId.New(),
            Short,
            EventId,
            new QrCodeState.Active(DateTimeOffset.UtcNow.AddHours(-1), 10, 0),
            createdAt: DateTimeOffset.UtcNow.AddDays(-1)
        );

        qr.CanUpload().ShouldBeFalse();
    }

    [Fact]
    public void CanUpload_False_WhenUploadCountAtMax()
    {
        var qr = QrCode.Rehydrate(
            QrCodeId.New(),
            Short,
            EventId,
            new QrCodeState.Active(null, MaxUploads: 5, UploadCount: 5),
            createdAt: DateTimeOffset.UtcNow.AddDays(-1)
        );

        qr.CanUpload().ShouldBeFalse();
    }

    [Fact]
    public void CanUpload_False_WhenDeactivated()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);
        qr.Deactivate();

        qr.CanUpload().ShouldBeFalse();
    }

    [Fact]
    public void CanUpload_True_WhenUnlimitedAndNoExpiry()
    {
        var qr = QrCode.Create(Short, EventId, maxUploads: 0);

        qr.CanUpload().ShouldBeTrue();
    }
}
