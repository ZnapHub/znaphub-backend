namespace ZnapHub.Application.Features.Photos.Dtos;

public sealed record PhotoContentDto(string FileName, Stream Stream, string ContentType);
