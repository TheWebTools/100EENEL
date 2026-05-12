namespace LogSentinel.Agent.Services.Alerts;

using LogSentinel.Agent.Models;

public interface IAlertSender
{
    string Channel { get; }
    Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default);
}
