namespace _100EENEL.Agent.Services.Alerts;

using _100EENEL.Agent.Models;

public interface IAlertSender
{
    string Channel { get; }
    Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default);
}
