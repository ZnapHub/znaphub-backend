using Microsoft.AspNetCore.Http;
using ZnapHub.Application.Abstractions.Messaging.Commands;

namespace ZnapHub.Application.Features.Photos.Commands.UploadPhoto;

public sealed record UploadPhotoCommand(Guid EventId, IFormFile File) : ICommand;
