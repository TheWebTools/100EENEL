using LogSentinel.Agent.Database;
using LogSentinel.Agent.Models;
using Microsoft.EntityFrameworkCore;

namespace LogSentinel.Agent.Services.Alerts;

public class AlertEngine
{
    private readonly LogDbContext _db;
    private readonly IEnumerable<IAlertSender> _senders;

    public AlertEngine(LogDbContext db, IEnumerable<IAlertSender> senders)
    {
        _db = db;
        _senders = senders;
    }

    public async Task ProcessNewLogsAsync(CancellationToken ct = default)
    {
        var rules = await _db.AlertRules
            .Where(r => r.Enabled)
            .ToListAsync(ct);

        if (!rules.Any())
            return;

        // Check logs from last 5 seconds
        var since = DateTime.UtcNow.AddSeconds(-5);

        var recentLogs = await _db.Logs
            .Where(l => l.Timestamp >= since)
            .ToListAsync(ct);

        foreach (var log in recentLogs)
        {
            foreach (var rule in rules)
            {
                if (!string.Equals(log.Severity, rule.Severity, StringComparison.OrdinalIgnoreCase))
                    continue;

                if (!string.IsNullOrWhiteSpace(rule.ContainsText) &&
                    !log.Message.Contains(rule.ContainsText, StringComparison.OrdinalIgnoreCase))
                    continue;

                var alert = new AlertEvent
                {
                    Timestamp = DateTime.UtcNow,
                    RuleName = rule.Name,
                    Severity = log.Severity,
                    Message = log.Message,
                    Source = log.Source
                };

                var sender = _senders.FirstOrDefault(s =>
                    string.Equals(s.Channel, rule.Channel, StringComparison.OrdinalIgnoreCase));

                if (sender != null)
                    _ = sender.SendAsync(alert, rule.Target, ct);
            }
        }
    }
}
