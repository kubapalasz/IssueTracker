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
    public class BoardJsonTests
    {
        [Fact]
        public void GetReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("Board").Result;

                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }
        
        [Fact]
        public void AfterPostingIssuesGetOnBoardReturnsCorrectAmountOfIssues()
        {
            using (var client = HttpClientFactory.Create())
            {
                var last = new
                {
                    title = "Last",
                    dueDate = DateTimeOffset.Now,
                    status = "Closed"
                };
                client.PostAsJsonAsync("", last).Wait();

                var middle = new
                {
                    title = "Middle",
                    dueDate = DateTimeOffset.Now.AddDays(-1),
                    status = "Closed"
                };
                client.PostAsJsonAsync("", middle).Wait();

                var first = new
                {
                    title = "First",
                    dueDate = DateTimeOffset.Now.AddDays(-2),
                    status = "Closed"
                };
                client.PostAsJsonAsync("", first).Wait();

                var response = client.GetAsync("Board").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.NotNull(actual);
                Assert.NotNull(actual.issues);
                Assert.Equal(actual.issues.Count, 3);
            }
        }
    }
}
