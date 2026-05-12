using System.Net;
using System.Net.Mail;
using _100EENEL.Agent.Models;

namespace _100EENEL.Agent.Services.Alerts;

public class EmailAlertSender : IAlertSender
{
    public string Channel => "Email";

    public async Task SendAsync(AlertEvent alert, string target, CancellationToken ct = default)
    {
        // target format: "smtp:host:port:username:password:to"
        var parts = target.Split(':');
        if (parts.Length != 6)
            return;

        var host = parts[1];
        var port = int.Parse(parts[2]);
        var user = parts[3];
        var pass = parts[4];
        var to = parts[5];

        var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = true
        };

        var msg = new MailMessage(user, to)
        {
            Subject = $"[{alert.Severity}] {alert.RuleName}",
            Body = $"{alert.Source}\n\n{alert.Message}"
        };

        await client.SendMailAsync(msg, ct);
    }
}
