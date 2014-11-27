using System.Collections.Generic;

namespace IssueTrackerApi
{
    public interface IIssueRepository
    {
        List<IssueModel> Get();
    }
}