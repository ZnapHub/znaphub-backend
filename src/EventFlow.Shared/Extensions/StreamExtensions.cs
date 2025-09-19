namespace EventFlow.Shared.Extensions;

public static class StreamExtensions
{
    public static Stream SetBeginStream(this Stream stream)
    {
        stream.Position = 0;
        return stream;
    }
}
