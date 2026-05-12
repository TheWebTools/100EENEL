using _100EENEL.Agent.Models;
using System.Diagnostics;

namespace _100EENEL.Agent.Parsers;

public class LinuxJournalctlParser : ILogParser
{
    public bool IsSupported() =>
        OperatingSystem.IsLinux();

    public IEnumerable<LogEntry> ReadLogs()
    {
        if (!IsSupported())
            yield break;

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "journalctl",
                Arguments = "-n 200 --no-pager",
                RedirectStandardOutput = true,
                UseShellExecute = false
            }
        };

        process.Start();

        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            yield return new LogEntry
            {
                Timestamp = DateTime.Now,
                Source = "journalctl",
                Message = line,
                Severity = line.Contains("error", StringComparison.OrdinalIgnoreCase) ? "Error" : "Info",
                Hostname = Environment.MachineName
            };
        }
    }
}
