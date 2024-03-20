using HttpRequestReporter.Common.Models;
using Microsoft.AspNetCore.Http;

namespace HttpRequestReporter.Service;

public interface IHttpRequestReporterService
{
    HttpRequestReport AnalyseLogFiles(List<IFormFile> logFiles);
}
