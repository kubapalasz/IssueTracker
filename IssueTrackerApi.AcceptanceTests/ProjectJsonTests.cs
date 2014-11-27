using Xunit;
using System.Net.Http;
using IssueTrackerApi.AcceptanceTests.Extensions;

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
                var project = client.CreateUniqueProject();

                // Act
                var response = client.PostAsJsonAsync("Project", project).Result;
                var actual = response.Content.ReadAsJsonAsync().Result;

                // Assert
                Assert.NotNull(actual);
                Assert.NotNull(actual.name);
                Assert.NotNull(actual.name.Value);
                Assert.Equal(project.Value("name"), actual.name.Value);
            }
        }

        [Fact]
        public void PostProjectWithNameThatExistsShouldFail()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Arrange
                var project = client.CreateUniqueProject();

                // Act
                var actualResponse = client.PostAsJsonAsync("Project", project).Result;

                // Assert
                Assert.Throws(typeof(HttpRequestException), () =>
                {
                    actualResponse.EnsureSuccessStatusCode();
                });
            }
        }
    }
}
