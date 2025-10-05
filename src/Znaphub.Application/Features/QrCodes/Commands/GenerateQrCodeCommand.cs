using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Domain.Features.Events.ValueObjects;

namespace ZnapHub.Application.Features.QrCodes.Commands;

public sealed record GenerateQrCodeCommand(Guid EventId, DateTimeOffset? ExpiresAt = null)
    : ICommand;
