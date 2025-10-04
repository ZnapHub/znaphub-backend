namespace ZnapHub.Domain.Features.QrCodes.Factories;

public interface IQrCodeUrlFactory
{
    string CreateUploadUrl(string shortId);
}
