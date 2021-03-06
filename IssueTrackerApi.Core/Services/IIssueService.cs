using System.Collections.Generic;

namespace IssueTrackerApi.Core.Services
{
    public interface IIssueService
    {
        List<IssueModel> Get();
        List<IssueModel> Get(int page, int pageSize);
        List<IssueModel> GetOrderedByDueDateAsc();
        IssueModel GetByNumber(string id);
        long TryAddIssue(IssueModel issueModel);
    }
}