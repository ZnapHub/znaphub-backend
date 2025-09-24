namespace ZnapHub.Application.Photos.Dtos;

public sealed record PhotoDto(
    Guid Id,
    string EventId,
    string FileName,
    string Url,
    DateTimeOffset UploadedAt
);
