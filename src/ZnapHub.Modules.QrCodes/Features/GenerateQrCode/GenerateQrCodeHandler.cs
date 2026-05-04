using Microsoft.Extensions.Options;
using ZnapHub.Modules.QrCodes.Domain.Entities;
using ZnapHub.Modules.QrCodes.Domain.Repositories;
using ZnapHub.Modules.QrCodes.Services;
using ZnapHub.Shared.Abstractions;
using ZnapHub.Shared.Options;

namespace ZnapHub.Modules.QrCodes.Features.GenerateQrCode;

internal sealed class GenerateQrCodeHandler
{
    private readonly IShortIdGenerator _shortIdGenerator;
    private readonly IQrCodeUrlFactory _urlFactory;
    private readonly IQrCodeRepository _qrCodes;
    private readonly StorageOptions _options;

    public GenerateQrCodeHandler(
        IShortIdGenerator shortIdGenerator,
        IQrCodeUrlFactory urlFactory,
        IQrCodeRepository qrCodes,
        IOptions<StorageOptions> options
    )
    {
        _shortIdGenerator = shortIdGenerator;
        _urlFactory = urlFactory;
        _qrCodes = qrCodes;
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
            command.EventId,
            _options.DefaultMaxUploads,
            command.ExpiresAt
        );

        await _qrCodes.AddAsync(qrCode, ct);

        var uploadUrl = _urlFactory.CreateUploadUrl(shortId);
        return Result.Success(new GenerateQrCodeResponse(uploadUrl));
    }
}
