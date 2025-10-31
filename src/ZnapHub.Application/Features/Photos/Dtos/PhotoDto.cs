namespace ZnapHub.Application.Features.Photos.Dtos;

public sealed record PhotoDto(
    Guid Id,
    Guid EventId,
    string FileName,
    string Url,
    DateTimeOffset UploadedAt
);
