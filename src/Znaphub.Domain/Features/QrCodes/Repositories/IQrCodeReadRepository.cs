using ZnapHub.Domain.Features.QrCodes.Entities;
using ZnapHub.Domain.Features.QrCodes.ValueObjects;

namespace ZnapHub.Domain.Features.QrCodes.Interfaces;

public interface IQrCodeReadRepository
{
    Task<QrCode?> GetAsync(QrCodeId id);
}
