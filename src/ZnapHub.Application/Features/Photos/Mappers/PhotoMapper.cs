using ZnapHub.Application.Features.Photos.Dtos;
using ZnapHub.Domain.Features.Photos.Entities;

namespace ZnapHub.Application.Features.Photos.Mappers;

public static class PhotoMapper
{
    public static PhotoDto ToDto(this Photo photo, string url) =>
        new(photo.Id, photo.EventId, photo.FileName, url, photo.CreatedAt);
}
