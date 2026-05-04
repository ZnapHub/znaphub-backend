using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Modules.QrCodes.Domain.Errors;

public static class QrCodeErrors
{
    public static readonly Error CannotIncrementInactive = new(
        "QrCode.CannotIncrementInactive: upload count can only be incremented while the QR code is active."
    );
}
