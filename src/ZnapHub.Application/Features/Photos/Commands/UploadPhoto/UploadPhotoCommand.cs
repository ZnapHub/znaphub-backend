using Microsoft.AspNetCore.Http;
using ZnapHub.Application.Abstractions.Messaging.Commands;
using ZnapHub.Application.Features.Photos.Dtos;

namespace ZnapHub.Application.Features.Photos.Commands.UploadPhoto;

public sealed record UploadPhotoCommand(string ShortId, PhotoContentDto File) : ICommand;
