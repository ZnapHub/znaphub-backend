using ZnapHub.Domain.Features.QrCodes.Entities;

namespace ZnapHub.Domain.Features.QrCodes.Repositories;

public interface IQrCodeWriteRepository
{
    Task AddAsync(QrCode qrCode);
}
