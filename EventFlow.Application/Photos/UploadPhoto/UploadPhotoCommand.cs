using EventFlow.Application.Abstractions.Messaging.Commands;
using Microsoft.AspNetCore.Http;

namespace EventFlow.Application.Photos.UploadPhoto;

public sealed record UploadPhotoCommand(string EventId, IFormFile File) : ICommand;
