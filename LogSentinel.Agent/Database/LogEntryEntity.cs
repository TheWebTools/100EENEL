using System;

namespace LogSentinel.Agent.Database;

public class LogEntryEntity
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Source { get; set; } = "";
    public string Message { get; set; } = "";
    public string Severity { get; set; } = "";
    public string Hostname { get; set; } = "";
}
