namespace ZnapHub.Shared.Extensions;

public static class StreamExtensions
{
    extension(Stream stream)
    {
        public Stream SetBeginStream()
        {
            stream.Position = 0;
            return stream;
        }
    }
}
