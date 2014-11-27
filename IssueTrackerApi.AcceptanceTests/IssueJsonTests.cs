using System;
using IssueTrackerApi.AcceptanceTests.Extensions;
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
        public void PostIssueSucceedsAndReturnsIssueNumber()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Addange 
                var project = client.CreateUniqueProject();

                // Act
                var issue = client.CreateUniqueIssue(project.Value("name"));

                // Assert
                Assert.NotNull(issue.number);
                Assert.NotNull(issue.number.Value);
            }
        }

        [Fact]
        public void GetIssueByNumberSucceeds()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Addange 
                var project = client.CreateUniqueProject();
                var issue = client.CreateUniqueIssue(project.Value("name"));

                // Act
                var url = "Issue/Get/" + issue.number.Value;
                var getResponse = client.GetAsync(url).Result;
                var actualIssue = getResponse.Content.Value;

                // Assert
                Assert.NotNull(actualIssue);
                Assert.Equal(actualIssue.Title, issue.title.Value);
                Assert.Equal(actualIssue.DueDate, issue.dueDate.Value);
                Assert.Equal(actualIssue.Status, issue.status.Value);
                Assert.Equal(actualIssue.Number, issue.number.Value);
            }
        }

        [Fact]
        public void AfterPostingEntryGetRootReturnsEntryInContent()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Addange 
                var project = client.CreateUniqueProject();
                var issue = client.CreateUniqueIssue(project.Value("name"));

                // Act
                var response = client.GetAsync("").Result;

                // Assert
                var actual = response.Content.ReadAsJsonAsync().Result;
                Assert.NotNull(actual);
                Assert.NotNull(actual.issues);
                Assert.Contains(issue, actual.issues);
            }
        }

        [Fact]
        public void PostIssueFailsWhenProjectWithGivenNameDoesNotExists()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Arrange
                var json = new
                {
                    title = "TDD Challange - PostIssueFailsWhenProjectNameIsEmptyOrInvalid",
                    dueDate = DateTimeOffset.Now,
                    status = "Open",
                    projectName = "SomeNotExistingProjectName"
                };

                // Act
                var actualResponse = client.PostAsJsonAsync("", json).Result;

                // Assert
                Assert.Throws(typeof(HttpRequestException), () =>
                {
                    actualResponse.EnsureSuccessStatusCode();
                });
            }
        }


        [Fact]
        public void SearchIssuesByTitlePart()
        {
            using (var client = HttpClientFactory.Create())
            {
                // Addange 
                var project1 = client.CreateUniqueProject();
                var project1Name = project1.Value("name");
                var project2 = client.CreateUniqueProject();
                var project2Name = project2.Value("name");
                var project3 = client.CreateUniqueProject();
                var project3Name = project3.Value("name");
                client.CreateUniqueIssue(project1Name, "Begin TeDeDe End");
                client.CreateUniqueIssue(project1Name, "' TeDeDe123321321 End");
                client.CreateUniqueIssue(project2Name, "Begin End TeDeDe");
                client.CreateUniqueIssue(project1Name, "Begin End No searched string");
                client.CreateUniqueIssue(project3Name, "Nothing important");
                client.CreateUniqueIssue(project3Name, "Nothing important than");
                client.CreateUniqueIssue(project3Name, "123435435");

                // Act
                var response = client.GetAsync("Issue/GetByTitle/TeDeDe").Result;

                // Assert
                var actual = response.Content.ReadAsJsonAsync().Result;
                var correctCount = 0;
                foreach (var issue in actual.issues)
                {
                    if (issue.projectName != project1Name && issue.projectName != project2Name)
                    {
                        continue;
                    }

                    Assert.Contains("TeDeDe", issue.title.Value.ToString());
                    correctCount++;
                }
                Assert.Equal(3, correctCount);
            }
        }
    }
}
