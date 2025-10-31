using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Features.QrCodes.Commands;

public sealed record GenerateQrCodeCommand(Guid EventId, DateTimeOffset? ExpiresAt = null)
    : ICommand;
