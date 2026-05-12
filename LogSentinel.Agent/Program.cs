using LogSentinel.Agent.Database;
using LogSentinel.Agent.Parsers;
using LogSentinel.Agent.Services;
using LogSentinel.Agent.Services.Alerts;   // <-- REQUIRED
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// SQLite database
builder.Services.AddDbContext<LogDbContext>();

// Log parsers
builder.Services.AddSingleton<ILogParser, WindowsEventLogParser>();
builder.Services.AddSingleton<ILogParser, LinuxJournalctlParser>();

// Alert senders
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAlertSender, DiscordAlertSender>();
builder.Services.AddSingleton<IAlertSender, SlackAlertSender>();
builder.Services.AddSingleton<IAlertSender, TelegramAlertSender>();
builder.Services.AddSingleton<IAlertSender, EmailAlertSender>();

// Alert engine
builder.Services.AddSingleton<AlertEngine>();

// Background log collector
builder.Services.AddSingleton<LogCollectorService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<LogCollectorService>());

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Auto-create SQLite DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogDbContext>();
    db.Database.EnsureCreated();
}

// API routes
app.MapControllers();

// Run agent
app.Run();
