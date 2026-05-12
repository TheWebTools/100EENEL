using Microsoft.EntityFrameworkCore;
using LogSentinel.Agent.Models;

namespace LogSentinel.Agent.Database;

public class LogDbContext : DbContext
{
    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
    public DbSet<LogEntryEntity> Logs => Set<LogEntryEntity>();

    public string DbPath { get; }

    public LogDbContext()
    {
        var folder = AppContext.BaseDirectory;
        DbPath = Path.Combine(folder, "logs.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogEntryEntity>().HasIndex(l => l.Timestamp);
        modelBuilder.Entity<LogEntryEntity>().HasIndex(l => l.Severity);
        modelBuilder.Entity<LogEntryEntity>().HasIndex(l => l.Source);

        modelBuilder.Entity<AlertRule>().HasIndex(r => r.Enabled);
    }
}
