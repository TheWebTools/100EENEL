using LogSentinel.Agent.Models;

namespace LogSentinel.Agent.Services.Alerts;

public class TelegramAlertSender : IAlertSender
{
    private readonly HttpClient _http;

    public TelegramAlertSender(HttpClient http)
    {
        _http = http;
    }

    public string Channel => "Telegram";

    public async Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default)
    {
        // target format: "BOT_TOKEN:CHAT_ID"
        var parts = target.Split(':');
        if (parts.Length != 2)
            return;

        var token = parts[0];
        var chatId = parts[1];

        var url =
            $"https://api.telegram.org/bot{token}/sendMessage?chat_id={chatId}&text=" +
            Uri.EscapeDataString($"[{alert.Severity}] {alert.RuleName}\n{alert.Source}\n{alert.Message}");

        await _http.GetAsync(url, ct);
    }
}
