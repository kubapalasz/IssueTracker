using System;
using Xunit;
using System.Net.Http;

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
        public void AfterPostingIssuesGetOnBoardReturnsCorrectAmountOfIssuesSortedByDate()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Arrange
                var responseBefore = client.GetAsync("Board").Result;
                var before = responseBefore.Content.ReadAsJsonAsync().Result;
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

                // Arrange
                var response = client.GetAsync("Board").Result;
                var actual = response.Content.ReadAsJsonAsync().Result;

                // Assert
                Assert.NotNull(actual);
                Assert.NotNull(actual.issues);
                Assert.True(before.issues.Count + 3 <= actual.issues.Count);
                var lastDueDate = DateTime.MinValue;
                foreach (var issue in actual.issues)
                {
                    if (lastDueDate > issue.dueDate.Value)
                    {
                        Assert.True(lastDueDate <= issue.dueDate.Value);
                    }
                }
            }
        }
    }
}
