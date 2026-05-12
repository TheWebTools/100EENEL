namespace LogSentinel.Agent.Models;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string Source { get; set; } = "";
    public string Message { get; set; } = "";
    public string Severity { get; set; } = "";
    public string Hostname { get; set; } = "";
}
