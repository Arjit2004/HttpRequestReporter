using HttpRequestReporter.Common.Models;
using System.Text.RegularExpressions;

namespace HttpRequestReporter.Common.Helpers;

public partial class LogParser
{
    /// <summary>
    /// To parse and extract the IP Address and URL visited from log record
    /// </summary>
    /// <param name="log">Log record containing information</param>
    /// <returns>If record is in correct format, IP Address and URL Visited, null otherwise</returns>
    public static HttpLog? ParseHttpLog(string log)
    {
        var match = MyRegex().Match(log);

        if (match.Success)
        {
            return new HttpLog(
                match.Groups[1].Value,
                match.Groups[2].Value);
        }

        return null;
    }

    [GeneratedRegex(@"^([\d\.]+) .* ""\w+ (\S+) .*""")]
    private static partial Regex MyRegex();
}
