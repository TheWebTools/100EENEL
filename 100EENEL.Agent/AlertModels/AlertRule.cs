namespace _100EENEL.Agent.Models;

public class AlertRule
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Severity { get; set; } = "Error"; // Error, Warning, Info
    public string? ContainsText { get; set; }       // match in Message
    public string Channel { get; set; } = "Discord"; // Discord, Slack, Email, Telegram
    public string Target { get; set; } = "";        // webhook URL, email, chat ID
    public bool Enabled { get; set; } = true;
}
