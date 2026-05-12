using _100EENEL.Agent.Models;

namespace _100EENEL.Agent.Parsers;

public interface ILogParser
{
    bool IsSupported();
    IEnumerable<LogEntry> ReadLogs();
}
