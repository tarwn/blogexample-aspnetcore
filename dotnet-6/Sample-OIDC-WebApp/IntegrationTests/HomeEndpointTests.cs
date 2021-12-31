using IntegrationTests.Utility;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System.Net;
using static IntegrationTests.Utility.CustomWebApplicationFactory;

namespace IntegrationTests
{
    [TestFixture]
    public class HomeEndpointTests
    {
        private CustomWebApplicationFactory _application;
        private WebApplicationFactoryClientOptions DefaultOptions = new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _application = new CustomWebApplicationFactory();
        }

        [Test]
        public async Task Index_Visitor_ReturnsOk()
        {
            var client = _application.CreateClient(DefaultOptions);

            var response = await client.GetAsync("/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Protected_LoggedInUser_ReturnsOk()
        {
            var client = _application.CreateLoggedInClient<GeneralUser>(DefaultOptions, 12345);

            var response = await client.GetAsync("/protected");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var body = await response.Content.ReadAsStringAsync();
            // there are better ways to do this, HTML parsing via 
            Assert.IsTrue(body.Contains("12345"));
        }

        [Test]
        public async Task Protected_Visitor_ReturnsUnauthorized()
        {
            var client = _application.CreateClient(DefaultOptions);

            var response = await client.GetAsync("/protected");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task Index_LoggedInUser_ReturnsOk()
        {
            var client = _application.CreateLoggedInClient<GeneralUser>(DefaultOptions, 12345);

            var response = await client.GetAsync("/");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task SampleEndpointWithoutExplicitAuthorization_Visitor_ReturnsUnauthorized()
        {
            var client = _application.CreateClient(DefaultOptions);

            var response = await client.GetAsync("/missingAuth");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task SampleEndpointWithoutExplicitAuthorization_LoggedInUser_ReturnsForbidden()
        {
            var client = _application.CreateLoggedInClient<GeneralUser>(DefaultOptions, 12345);

            var response = await client.GetAsync("/missingAuth");

            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}