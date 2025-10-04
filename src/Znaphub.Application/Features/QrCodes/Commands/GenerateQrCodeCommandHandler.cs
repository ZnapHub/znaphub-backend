using ZnapHub.Application.Abstractions.Data;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Domain.Features.QrCodes.Repositories;
using ZnapHub.Shared.Abstractions;

namespace ZnapHub.Application.Features.QrCodes.Commands;

public sealed class GenerateQrCodeCommandHandler : ICommandHandler<GenerateQrCodeCommand>
{
    private readonly IQrCodeWriteRepository _qrCodeWriteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateQrCodeCommandHandler(
        IQrCodeWriteRepository qrCodeWriteRepository,
        IUnitOfWork unitOfWork
    )
    {
        _qrCodeWriteRepository = qrCodeWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(GenerateQrCodeCommand command)
    {
        throw new NotImplementedException();
    }
}
