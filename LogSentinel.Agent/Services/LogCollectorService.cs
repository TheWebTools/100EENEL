using LogSentinel.Agent.Models;
using LogSentinel.Agent.Parsers;
using LogSentinel.Agent.Database;
using Microsoft.Extensions.Hosting;

namespace LogSentinel.Agent.Services;

public class LogCollectorService : BackgroundService
{
    private readonly IEnumerable<ILogParser> _parsers;
    private readonly IServiceProvider _provider;

    public LogCollectorService(IEnumerable<ILogParser> parsers, IServiceProvider provider)
    {
        _parsers = parsers;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LogDbContext>();

            foreach (var parser in _parsers)
            {
                if (!parser.IsSupported())
                    continue;

                foreach (var log in parser.ReadLogs())
                {
                    db.Logs.Add(new LogEntryEntity
                    {
                        Timestamp = log.Timestamp,
                        Source = log.Source,
                        Message = log.Message,
                        Severity = log.Severity,
                        Hostname = log.Hostname
                    });
                }
            }

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
