namespace ZnapHub.Application.Features.Photos.Dtos;

public sealed record PhotoDto(
    Guid Id,
    string EventId,
    string FileName,
    string Url,
    DateTimeOffset UploadedAt
);
