using _100EENEL.Agent.Services;
using Microsoft.AspNetCore.Mvc;

namespace _100EENEL.Agent.Controllers;

[ApiController]
[Route("api/logs")]
public class LogsController : ControllerBase
{
    private readonly LogQueryService _query;

    public LogsController(LogQueryService query)
    {
        _query = query;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs(
        int page = 1,
        int pageSize = 100,
        string? severity = null,
        string? source = null,
        string? search = null)
    {
        var logs = await _query.GetLogs(page, pageSize, severity, source, search);
        return Ok(logs);
    }
}
