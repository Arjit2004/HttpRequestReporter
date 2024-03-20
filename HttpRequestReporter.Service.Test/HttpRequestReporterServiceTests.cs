using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace HttpRequestReporter.Service.Test;

public class HttpRequestReporterServiceTests
{
    private readonly Mock<ILogger<HttpRequestReporterService>> _loggerMock = new();

    [Fact]
    public void AnalyseLogFiles_EmptyFilesUploaded_ThrowsException()
    {
        var service = new HttpRequestReporterService(_loggerMock.Object);

        var input = new List<IFormFile>
        {
            new Mock<IFormFile>().Object
        };

        Assert.Throws<InvalidDataException>(() => service.AnalyseLogFiles(input));
    }

    [Fact]
    public void AnalyseLogFiles_WrongFormatFileUploaded_ThrowsException()
    {
        var service = new HttpRequestReporterService(_loggerMock.Object);

        Assert.Throws<InvalidDataException>(() => service.AnalyseLogFiles(CreateWrongFiles()));
    }

    [Fact]
    public void AnalyseLogFiles_FileUploaded_ReportReturned()
    {
        var service = new HttpRequestReporterService(_loggerMock.Object);

        var report = service.AnalyseLogFiles(CreateFiles());

        Assert.NotNull(report);
        Assert.Equal(2, report.UniqueHttpCount);
        Assert.Equal(2, report.MostVisitedURLs.Count);
        Assert.Equal(2, report.MostActiveIPs.Count);
    }

    private static List<IFormFile> CreateFiles()
    {
        return new List<IFormFile>()
        {
            new MockFormFile("1.txt", "177.71.128.21 - - [10/Jul/2018:22:21:28 +0200] \"GET /intranet-analytics/ HTTP/1.1\" 200 3574"),
            new MockFormFile("2.txt", "168.41.191.40 - - [09/Jul/2018:10:11:30 +0200] \"GET http://example.net/faq/ HTTP/1.1\" 200 3574")
        };
    }

    private static List<IFormFile> CreateWrongFiles()
    {
        return new List<IFormFile>()
        {
            new MockFormFile("1.txt", "bsfgfh"),
            new MockFormFile("2.txt", "afQDQGQRYJTKL")
        };
    }
}