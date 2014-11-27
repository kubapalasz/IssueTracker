using System.Collections.Generic;

namespace IssueTrackerApi
{
    public static class FakeDatabase
    {
        public static readonly List<IssueModel> Issues = new List<IssueModel>();
        public static readonly List<ProjectModel> Projects = new List<ProjectModel>();
    }
}
