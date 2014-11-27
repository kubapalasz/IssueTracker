using System.Collections.Generic;

namespace IssueTrackerApi
{
    public interface IIssueService
    {
        List<IssueModel> Get();
        List<IssueModel> Get(int page, int pageSize);
        List<IssueModel> GetOrderedByDueDateAsc();
        IssueModel GetByNumber(string id);
    }
}