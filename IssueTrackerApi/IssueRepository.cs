using System.Collections.Generic;

namespace IssueTrackerApi
{
    public class IssueRepository : IIssueRepository
    {
        public List<IssueModel> Get()
        {
            return FakeDatabase.Issues;
        }
    }
}
