namespace LogSentinel.Agent.Models;

public class AlertEvent
{
    public DateTime Timestamp { get; set; }
    public string RuleName { get; set; } = "";
    public string Severity { get; set; } = "";
    public string Message { get; set; } = "";
    public string Source { get; set; } = "";
}
