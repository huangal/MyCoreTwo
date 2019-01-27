using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyCoreTwo.IntegrationTests
{
    [TestClass]
    public class ContactsIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public ContactsIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            _client = _server.CreateClient();

        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
        }

        [TestMethod]
        public async void WhenSetGetMehtodToRetriveContactReturnListOfContacts()
        {

            var request = new HttpRequestMessage(new HttpMethod("GET"), "api/Contacts");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            Assert.Equals(HttpStatusCode.OK, response.StatusCode);


        }
    }
}
