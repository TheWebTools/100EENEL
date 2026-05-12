using _100EENEL.Agent.Database;
using Microsoft.EntityFrameworkCore;

namespace _100EENEL.Agent.Services;

public class LogQueryService
{
    private readonly LogDbContext _db;

    public LogQueryService(LogDbContext db)
    {
        _db = db;
    }

    public async Task<List<LogEntryEntity>> GetLogs(
        int page = 1,
        int pageSize = 100,
        string? severity = null,
        string? source = null,
        string? search = null)
    {
        var query = _db.Logs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(severity))
            query = query.Where(l => l.Severity == severity);

        if (!string.IsNullOrWhiteSpace(source))
            query = query.Where(l => l.Source.Contains(source));

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(l => l.Message.Contains(search));

        return await query
            .OrderByDescending(l => l.Timestamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountLogs(string? severity = null)
    {
        var query = _db.Logs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(severity))
            query = query.Where(l => l.Severity == severity);

        return await query.CountAsync();
    }

    public async Task<Dictionary<string, int>> CountBySeverity()
    {
         return await _db.Logs
        .GroupBy(l => l.Severity)
        .Select(g => new { g.Key, Count = g.Count() })
        .ToDictionaryAsync(x => x.Key, x => x.Count);
    }

    public async Task<Dictionary<DateTime, int>> CountByHour()
    {
         return await _db.Logs
        .GroupBy(l => new DateTime(l.Timestamp.Year, l.Timestamp.Month, l.Timestamp.Day, l.Timestamp.Hour, 0, 0))
        .Select(g => new { g.Key, Count = g.Count() })
        .ToDictionaryAsync(x => x.Key, x => x.Count);
   }

}
