namespace ZnapHub.Shared.Options;

public sealed record StorageOptions
{
    public required string BaseUploadUrl { get; init; }

    public int DefaultMaxUploads { get; init; }
}
