using System.Collections.Generic;
using IssueTrackerApi.Core;

namespace IssueTrackerApi.Services
{
    public static class FakeDatabase
    {
        public static readonly List<IssueModel> Issues = new List<IssueModel>();
        public static readonly List<Project> Projects = new List<Project>();
    }
}
