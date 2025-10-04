using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Domain.Features.QrCodes.Services;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.QrCodes.Commands;

public sealed class GenerateQrCodeCommandHandler : ICommandHandler<GenerateQrCodeCommand>
{
    private readonly IShortIdGenerator _shortIdGenerator;
    private readonly IQrCodeWriteRepository _qrCodeWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateQrCodeCommandHandler(
        IShortIdGenerator shortIdGenerator,
        IQrCodeWriteRepository qrCodeWriteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _shortIdGenerator = shortIdGenerator;
        _qrCodeWriteRepository = qrCodeWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(GenerateQrCodeCommand command)
    {
        var shortId = await _shortIdGenerator.GenerateUniqueAsync();

        var qrCode = QrCode.Create(shortId, command.EventId, 500);

        await _qrCodeWriteRepository.AddAsync(qrCode);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
