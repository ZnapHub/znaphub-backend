using Microsoft.Extensions.Options;
using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Domain.Features.Events.ValueObjects;
using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.Factories;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Domain.Features.QrCodes.Services;
using ZnapHub.Shared.Abstractions;
using ZnapHub.Shared.Options;

namespace ZnapHub.Application.Features.QrCodes.Commands;

public sealed class GenerateQrCodeCommandHandler
    : ICommandHandler<GenerateQrCodeCommand, GenerateQrCodeResponse>
{
    private readonly IShortIdGenerator _shortIdGenerator;
    private readonly IQrCodeUrlFactory _qrCodeUrlFactory;
    private readonly IQrCodeWriteRepository _qrCodeWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly StorageOptions _options;

    public GenerateQrCodeCommandHandler(
        IShortIdGenerator shortIdGenerator,
        IQrCodeUrlFactory qrCodeUrlFactory,
        IQrCodeWriteRepository qrCodeWriteRepository,
        IUnitOfWork unitOfWork,
        IOptions<StorageOptions> options
    )
    {
        _shortIdGenerator = shortIdGenerator;
        _qrCodeUrlFactory = qrCodeUrlFactory;
        _qrCodeWriteRepository = qrCodeWriteRepository;
        _unitOfWork = unitOfWork;
        _options = options.Value;
    }

    public async Task<Result<GenerateQrCodeResponse>> HandleAsync(
        GenerateQrCodeCommand command,
        CancellationToken ct = default
    )
    {
        var shortId = await _shortIdGenerator.GenerateUniqueAsync(ct);
        var qrCode = QrCode.Create(
            shortId,
            EventId.FromGuid(command.EventId),
            _options.DefaultMaxUploads,
            command.ExpiresAt
        );

        await _qrCodeWriteRepository.AddAsync(qrCode, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        var uploadUrl = _qrCodeUrlFactory.CreateUploadUrl(shortId);
        return Result.Success(new GenerateQrCodeResponse(uploadUrl));
    }
}
