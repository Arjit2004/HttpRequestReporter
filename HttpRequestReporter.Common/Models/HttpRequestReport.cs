namespace HttpRequestReporter.Common.Models;

public record HttpRequestReport(
    int UniqueHttpCount,
    List<string> MostVisitedURLs,
    List<string> MostActiveIPs);
