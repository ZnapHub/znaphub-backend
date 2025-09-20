using Microsoft.AspNetCore.Http;
using ZnapHub.Applicationz.Abstractions.Messaging.Commands;

namespace ZnapHub.Applicationz.Photos.UploadPhoto;

public sealed record UploadPhotoCommand(Guid EventId, IFormFile File) : ICommand;
