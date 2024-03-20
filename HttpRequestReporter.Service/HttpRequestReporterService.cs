using HttpRequestReporter.Common.Helpers;
using HttpRequestReporter.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HttpRequestReporter.Service;

public class HttpRequestReporterService : IHttpRequestReporterService
{
    private readonly ILogger<HttpRequestReporterService> _logger;

    public HttpRequestReporterService(ILogger<HttpRequestReporterService> logger)
    {
            _logger = logger;
    }

    /// <summary>
    /// To read the uploaded files, and create the report.
    /// </summary>
    /// <param name="logFiles">Files uploaded by user</param>
    /// <returns>Report based on the data uploaded</returns>
    /// <exception cref="Exception">Returning when files are empty, or data is not formatted properly</exception>
    public HttpRequestReport AnalyseLogFiles(List<IFormFile> logFiles)
    {
        _logger.LogInformation("Request received in the service layer.");

        var logs = new List<HttpLog>();

        try
        {
            foreach (var logFile in logFiles)
            {
                using var reader = new StreamReader(logFile.OpenReadStream());
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var log = LogParser.ParseHttpLog(line!);

                    if (log != null)
                    {
                        logs.Add(log);
                    }
                }
            }

            if (logs.Count > 0)
            {
                _logger.LogInformation("Files successfully processed, returning report data.");

                return new HttpRequestReport(
                    logs.Select(x => x.IPAddress).Distinct().Count(),
                    logs.GroupBy(x => x.URL).OrderByDescending(y => y.Count()).Take(3).Select(z => z.Key).ToList(),
                    logs.GroupBy(x => x.IPAddress).OrderByDescending(y => y.Count()).Take(3).Select(z => z.Key).ToList());
            }

            throw new Exception();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception thrown, excption message: \"{0}\"", ex.Message);
            throw new InvalidDataException("Either files uploaded were empty, or data present wasn't in the right format", ex);
        }
    }
}
