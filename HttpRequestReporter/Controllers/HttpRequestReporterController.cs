using HttpRequestReporter.Service;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequestReporter.Controllers;

[ApiController]
[Route("[controller]")]
public class HttpRequestReporterController : ControllerBase
{
    private readonly ILogger<HttpRequestReporterController> _logger;
    private readonly IHttpRequestReporterService _service;

    public HttpRequestReporterController(
        ILogger<HttpRequestReporterController> logger,
        IHttpRequestReporterService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// To generate a report based on the HTTP logs.
    /// </summary>
    /// <param name="logFiles">Files containing HTTP logs</param>
    /// <returns>The Report, or exception</returns>
    [HttpPost("upload-logs", Name = "PostLogFiles")]
    public IActionResult AnalyseHttpLogs([FromQuery] List<IFormFile> logFiles)
    {
        _logger.LogInformation("Request to analyse log file received");

        // validating if file(s) are present
        if (logFiles == null || logFiles.Count == 0)
            return BadRequest("No file uploaded");

        // get response from service and return it
        var response = _service.AnalyseLogFiles(logFiles);
        _logger.LogDebug("successfully executed");
        return Ok(response);
    }
}
