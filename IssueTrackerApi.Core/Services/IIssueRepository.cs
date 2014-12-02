using System.Collections.Generic;

namespace IssueTrackerApi.Core.Services
{
    public interface IIssueRepository
    {
        List<IssueModel> Get();

        long Save(IssueModel issueModel);
    }
}