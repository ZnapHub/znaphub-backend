using Microsoft.AspNetCore.Http;
using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Photos.UploadPhoto;

public sealed record UploadPhotoCommand(Guid EventId, IFormFile File) : ICommand;
