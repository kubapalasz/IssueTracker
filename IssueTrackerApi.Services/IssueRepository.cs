using System.Collections.Generic;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;

namespace IssueTrackerApi.Services
{
    public class IssueRepository : IIssueRepository
    {
        public List<IssueModel> Get()
        {
            return FakeDatabase.Issues;
        }

        public void Save(IssueModel issueModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
