using Xunit;
using System.Net.Http;

namespace IssueTrackerApi.AcceptanceTests
{
    public class ProjectJsonTests
    {
        [Fact]
        public void GetReturnsResponseWithCorrectStatusCode()
        {
            using (var client = HttpClientFactory.Create())
            {
                var response = client.GetAsync("Project").Result;

                Assert.True(
                    response.IsSuccessStatusCode,
                    "Actual status code: " + response.StatusCode);
            }
        }
        
        [Fact]
        public void PostProjectSucceedsAndReturnsCorrectData()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Arrange
                var project = new
                {
                    name = "CBE",
                    description = "Core Business En."
                };

                // Act
                var response = client.PostAsJsonAsync("Project", project).Result;
                var actual = response.Content.ReadAsJsonAsync().Result;

                // Assert
                Assert.NotNull(actual);
                Assert.NotNull(actual.name);
                Assert.NotNull(actual.name.Value);
                Assert.Equal("CBE", actual.name.Value);
            }
        }
    }
}
