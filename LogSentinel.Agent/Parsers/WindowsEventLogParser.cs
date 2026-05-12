using LogSentinel.Agent.Models;
using System.Diagnostics.Eventing.Reader;

namespace LogSentinel.Agent.Parsers;

public class WindowsEventLogParser : ILogParser
{
    public bool IsSupported() =>
        OperatingSystem.IsWindows();

    public IEnumerable<LogEntry> ReadLogs()
    {
        if (!IsSupported())
            yield break;

        var query = new EventLogQuery("Application", PathType.LogName);
        var reader = new EventLogReader(query);

        EventRecord? record;
        while ((record = reader.ReadEvent()) != null)
        {
            yield return new LogEntry
            {
                Timestamp = record.TimeCreated ?? DateTime.Now,
                Source = record.ProviderName ?? "Unknown",
                Message = record.FormatDescription() ?? "",
                Severity = record.LevelDisplayName ?? "Info",
                Hostname = Environment.MachineName
            };
        }
    }
}
