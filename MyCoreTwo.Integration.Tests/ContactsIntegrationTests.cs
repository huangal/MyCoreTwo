using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MyCoreTwo.Integration.Tests
{
    public class ContactsIntegrationTests
    {
        private readonly ITestContext _context;

        public ContactsIntegrationTests()
        {
            _context = new TestContext();
        }

        //[Fact]
        //public async Task PingReturnsOkResponse()
        //{
        //    var response = await _context.Client.GetAsync("api/Healthcheck/ping");

        //    response.EnsureSuccessStatusCode();

        //    //response.StatusCode.Should().Be(HttpStatusCode.OK);
        //   // Debug.WriteLine(response.StatusCode);


        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}


        [Fact]         public async Task WhenSetGetMehtodToRetriveContactReturnListOfContactsAsync()         {             //var request = new HttpRequestMessage(new HttpMethod("GET"), "api/Contacts");              //var response = await _client.SendAsync(request);
            var response = await _context.Client.GetAsync("api/Contacts");              response.EnsureSuccessStatusCode();              Assert.Equal(HttpStatusCode.OK, response.StatusCode);         }


        [Theory]
        [InlineData("Full")]
        public async Task GentSingleContact(string resource)
        {
            //var request = new HttpRequestMessage(new HttpMethod("GET"), "api/Contacts");

            //var response = await _client.SendAsync(request);
            var response = await _context.Client.GetAsync($"api/Contacts/{resource}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData(1)]
        public async Task Test(int id)
        {
            //var request = new HttpRequestMessage(new HttpMethod("GET"), "api/Contacts");

            //var response = await _client.SendAsync(request);
            var response = await _context.Client.GetAsync($"api/Values/{id}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
