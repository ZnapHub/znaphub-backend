using ZnapHub.Domain.Features.QrCodes.Entities;

namespace ZnapHub.Domain.Features.QrCodes.Interfaces;

public interface IQrCodeWriteRepository
{
    Task AddAsync(QrCode qrCode);
}
