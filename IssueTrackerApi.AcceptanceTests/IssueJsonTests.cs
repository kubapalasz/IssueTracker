using System;
using Xunit;
using System.Net.Http;

namespace IssueTrackerApi.AcceptanceTests
{
    public class IssueJsonTests
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
        public void PostIssueSucceeds()
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
        public void PostIssueSucceedsAndReturnsIssueNumber()
        {
            using (var client = HttpClientFactory.Create())
            {
                var json = new
                {
                    title = "TDD Challange - check issue number setting",
                    dueDate = DateTimeOffset.Now,
                    status = "Open"
                };

                var response = client.PostAsJsonAsync("", json).Result;
                var actual = response.Content.ReadAsJsonAsync().Result;

                Assert.NotNull(actual.number);
                Assert.NotNull(actual.number.Value);
            }
        }

        [Fact]
        public void GetIssueByNumberSucceeds()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Arrange
                var title = "TDD Challange - check issue number setting";
                var dueDate = DateTimeOffset.Now;
                var status = "Open";
                string number;

                var json = new
                {
                    title,
                    dueDate,
                    status
                };
                var postResponse = client.PostAsJsonAsync("", json).Result;
                var addedIssue = postResponse.Content.ReadAsJsonAsync().Result;
                number = addedIssue.number;

                // Act
                var url = "Issue/" + addedIssue.number.Value;
                var getResponse = client.GetAsync(url).Result;
                var actualIssue = getResponse.Content.Value;

                // Assert
                Assert.NotNull(actualIssue);
                Assert.Equal(actualIssue.Title, title);
                Assert.Equal(actualIssue.DueDate, dueDate);
                Assert.Equal(actualIssue.Status, status);
                Assert.Equal(actualIssue.Number, number);
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
                var postResponse = client.PostAsJsonAsync("", json).Result;
                var expected = postResponse.Content.ReadAsJsonAsync().Result;

                var response = client.GetAsync("").Result;

                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.NotNull(actual);
                Assert.NotNull(actual.issues);
                Assert.Contains(expected, actual.issues);
            }
        }
    }
}
