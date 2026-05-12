using Microsoft.EntityFrameworkCore;

namespace LogSentinel.Agent.Database;

public static class LogDbContextExtensions
{
    public static void ApplyIndexes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntryEntity>()
            .HasIndex(l => l.Timestamp);

        modelBuilder.Entity<LogEntryEntity>()
            .HasIndex(l => l.Severity);

        modelBuilder.Entity<LogEntryEntity>()
            .HasIndex(l => l.Source);
    }
}
