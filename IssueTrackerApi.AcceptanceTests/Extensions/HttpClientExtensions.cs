using System;
using System.Net.Http;

namespace IssueTrackerApi.AcceptanceTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static dynamic CreateUniqueIssue(this HttpClient httpClient, object projectName, string title = "TDD Challange - check issue number setting")
        {
            var json = new
            {
                title,
                dueDate = DateTimeOffset.Now,
                status = "Open",
                projectName
            };

            var response = httpClient.PostAsJsonAsync("", json).Result;
            var actual = response.Content.ReadAsJsonAsync().Result;

            return actual;
        }

        public static object CreateUniqueProject(this HttpClient httpClient)
        {
            var project = new
            {
                name = "CBE" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Minute + DateTime.Now.Second +
                       DateTime.Now.Millisecond,
                description = "Core Business En."
            };
            var response = httpClient.PostAsJsonAsync("Project", project).Result;
            response.EnsureSuccessStatusCode();

            return project;
        }
    }
}
