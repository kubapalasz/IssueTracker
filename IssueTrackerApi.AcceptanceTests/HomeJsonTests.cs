using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Configuration;
using Simple.Data;
using System.Dynamic;
using Xunit.Extensions;

namespace IssueTrackerApi.AcceptanceTests
{
    public class HomeJsonTests
    {
        [Fact]
        public void GetReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("").Result;

                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }


        [Fact]
        public void PostEntrySucceeds()
        {
            using (var client = HttpClientFactory.Create())
            {
                // title, text description, due date, status. Issue status: open, closed, in progress
                var json = new
                {
                    title = "TDD Challange",
                    dueDate = DateTimeOffset.Now,
                    status = "Open"
                };

                var response = client.PostAsJsonAsync("", json).Result;

                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }
        
        [Fact]
        public void AfterPostingEntryGetRootReturnsEntryInContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                var json = new
                {
                    title = "TDD Challange 2",
                    dueDate = DateTimeOffset.Now,
                    status = "Closed"
                };
                var expected = json.ToJObject();
                client.PostAsJsonAsync("", json).Wait();

                var response = client.GetAsync("").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.Contains(expected, actual.issues);
            }
        }
    }
}
