using Microsoft.AspNetCore.Http;
using System.Text;

namespace HttpRequestReporter.Service.Test;

public class MockFormFile : IFormFile
{
    private readonly string _filename; 
    private readonly string _content;

    public MockFormFile(
        string filename,
        string content)
    {
        _filename = filename;
        _content = content;
    }

    public string ContentType => throw new NotImplementedException();

    public string ContentDisposition => throw new NotImplementedException();

    public IHeaderDictionary Headers => throw new NotImplementedException();

    public long Length => throw new NotImplementedException();

    public string Name => throw new NotImplementedException();

    public string FileName => _filename;

    public void CopyTo(Stream target)
    {
        throw new NotImplementedException();
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Stream OpenReadStream()
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(_content));
    }
}
