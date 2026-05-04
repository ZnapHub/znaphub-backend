using ZnapHub.Modules.QrCodes.Domain.ValueObjects;
using ZnapHub.Modules.QrCodes.Services;

namespace ZnapHub.Modules.QrCodes.Tests.Fakes;

internal sealed class FakeShortIdGenerator : IShortIdGenerator
{
    private readonly Queue<ShortId> _queue;

    public FakeShortIdGenerator(params ShortId[] shortIds) => _queue = new Queue<ShortId>(shortIds);

    public Task<ShortId> GenerateUniqueAsync(CancellationToken ct = default) =>
        Task.FromResult(_queue.Dequeue());
}
