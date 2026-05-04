namespace ZnapHub.Modules.QrCodes.Features.GenerateQrCode;

public sealed record GenerateQrCodeCommand(Guid EventId, DateTimeOffset? ExpiresAt = null);
