using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.SelfHost;
using NUnit.Framework;

namespace IssueTracker.Api.AcceptanceTests
{
    public class HomeJsonTests
    {
        [Test]
        public void GetReturnsResponseWithCorrectStatusCode()
        {
            var baseAddress = new Uri("http://localhost:9876");
            var configuration = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrap().Configure(configuration);
            var server = new HttpSelfHostServer(configuration);

            using (var client = new HttpClient(server))
            {
                client.BaseAddress = baseAddress;

                var response = client.GetAsync("").Result;

                Assert.That(response.IsSuccessStatusCode, Is.True);
            }
        }
    }
}
