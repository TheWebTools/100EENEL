using System.Net.Http.Json;
using LogSentinel.Agent.Models;

namespace LogSentinel.Agent.Services.Alerts;

public class SlackAlertSender : IAlertSender
{
    private readonly HttpClient _http;

    public SlackAlertSender(HttpClient http)
    {
        _http = http;
    }

    public string Channel => "Slack";

    public async Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default)
    {
        var payload = new
        {
            text = $"[{alert.Severity}] {alert.RuleName}\n{alert.Source}\n{alert.Message}"
        };

        await _http.PostAsJsonAsync(target, payload, ct);
    }
}
