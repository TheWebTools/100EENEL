using System.Net.Http.Json;
using LogSentinel.Agent.Models;

namespace LogSentinel.Agent.Services.Alerts;

public class DiscordAlertSender : IAlertSender
{
    private readonly HttpClient _http;

    public DiscordAlertSender(HttpClient http)
    {
        _http = http;
    }

    public string Channel => "Discord";

    public async Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default)
    {
        var payload = new
        {
            content = $"**[{alert.Severity}] {alert.RuleName}**\n`{alert.Source}`\n{alert.Message}"
        };

        await _http.PostAsJsonAsync(target, payload, ct);
    }
}
