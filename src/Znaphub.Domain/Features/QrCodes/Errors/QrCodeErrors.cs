using ZnapHub.Domain.Features.QrCodes.Messages;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Domain.Features.QrCodes.Errors;

public static class QrCodeErrors
{
    public static Error CannotIncrementInactive => new(QrCodeErrorMessages.CannotIncrementInactive);
}
