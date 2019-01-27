using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace MyCoreTwo.Integration.Tests
{
    public class TestContext : ITestContext     {         private TestServer _server;         public HttpClient Client { get; private set; }          public TestContext()         {             SetUpClient();         }          private void SetUpClient()         {             _server = new TestServer(new WebHostBuilder()                 .UseStartup<Startup>());              Client = _server.CreateClient();         }     } 
}
