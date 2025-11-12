using ZnapHub.Application.Features.Photos.Dtos;
using ZnapHub.Domain.Features.Photos.Entities;

namespace ZnapHub.Application.Features.Photos.Mappers;

internal static class PhotoMapper
{
    extension(Photo photo)
    {
        internal PhotoDto ToDto(string url) =>
            new(photo.Id, photo.EventId, photo.FileName, url, photo.CreatedAt);
    }
}
