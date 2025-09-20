namespace Znaphub.Sharedz.Extensions;

public static class StreamExtensions
{
    public static Stream SetBeginStream(this Stream stream)
    {
        stream.Position = 0;
        return stream;
    }
}
