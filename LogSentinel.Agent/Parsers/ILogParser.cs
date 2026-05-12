using LogSentinel.Agent.Models;

namespace LogSentinel.Agent.Parsers;

public interface ILogParser
{
    bool IsSupported();
    IEnumerable<LogEntry> ReadLogs();
}
