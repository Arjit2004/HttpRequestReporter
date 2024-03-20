using HttpRequestReporter.Common.Models;
using HttpRequestReporter.Controllers;
using HttpRequestReporter.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HttpRequestReporter.Test;

public class HttpRequestReporterControllerTests
{
    private readonly Mock<ILogger<HttpRequestReporterController>> _loggerMock = new();
    private readonly Mock<IHttpRequestReporterService> _serviceMock = new();    

    [Fact]
    public void AnalyseHttpLogs_NoFileUploaded_ReturnsBadRequest()
    {
        var controller = new HttpRequestReporterController(
            _loggerMock.Object,
            _serviceMock.Object);

        var result = controller.AnalyseHttpLogs([]);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void AnalyseHttpLogs_EmptyFileUploaded_ThrowsException()
    {
        var controller = new HttpRequestReporterController(
           _loggerMock.Object,
           _serviceMock.Object);

        _serviceMock.Setup((c) => c.AnalyseLogFiles(It.IsAny<List<IFormFile>>())).Throws(new Exception());

        var input = new List<IFormFile>
        {
            new Mock<IFormFile>().Object
        };

        Assert.Throws<Exception>(() => controller.AnalyseHttpLogs(input));
    }

    [Fact]
    public void AnalyseHttpLogs_FileUploaded_ReturnsOkWithReport()
    {
        var controller = new HttpRequestReporterController(
           _loggerMock.Object,
           _serviceMock.Object);

        _serviceMock.Setup((c) => c.AnalyseLogFiles(It.IsAny<List<IFormFile>>())).Returns(GetValidResponse());

        var input = new List<IFormFile>
        {
            new Mock<IFormFile>().Object
        };

        var response = controller.AnalyseHttpLogs(input);

        var result = Assert.IsType<OkObjectResult>(response);
        var report = Assert.IsType<HttpRequestReport>(result.Value);
        Assert.Equal(10, report.UniqueHttpCount);
        Assert.Equal(3, report.MostActiveIPs.Count);
        Assert.Equal(3, report.MostVisitedURLs.Count);
    }

    private static HttpRequestReport GetValidResponse()
    {
        return new HttpRequestReport(
            10,
            ["0.0.0.0", "10.10.10.10", "10.100.124.10"],
            ["aaa", "bbb", "ccc"]);
    }
}