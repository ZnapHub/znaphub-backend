using Microsoft.EntityFrameworkCore;

namespace ZnapHub.Modules.QrCodes.Data;

public sealed class QrCodesDbContext : DbContext
{
    public QrCodesDbContext(DbContextOptions<QrCodesDbContext> options)
        : base(options) { }

    internal DbSet<QrCodeEntity> QrCodes => Set<QrCodeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("qrcodes");
        modelBuilder.ApplyConfiguration(new QrCodeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
