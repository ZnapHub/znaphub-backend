namespace ZnapHub.Modules.QrCodes.Services;

internal interface IQrCodeUrlFactory
{
    Uri CreateUploadUrl(string shortId);
}
