using System.Net.Http;

namespace MyCoreTwo.Integration.Tests
{
    public interface ITestContext
    {
        HttpClient Client { get; }
    }
}