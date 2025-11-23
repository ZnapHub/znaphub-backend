namespace ZnapHub.Domain.Features.QrCodes.Factories;

public interface IQrCodeUrlFactory
{
    Uri CreateUploadUrl(string shortId);
}
