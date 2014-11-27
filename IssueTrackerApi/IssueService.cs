using System.Collections.Generic;
using System.Linq;

namespace IssueTrackerApi
{
    public class IssueService : IIssueService
    {
        public IIssueRepository _issueRepository { get; set; }

        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public List<IssueModel> Get()
        {
            return _issueRepository.Get();
        }

        public List<IssueModel> Get(int page, int pageSize)
        {
            var all = Get();
            return all.Skip(page-1*pageSize).Take(pageSize).ToList();
        }

        public List<IssueModel> GetOrderedByDueDateAsc()
        {
            return Get().OrderBy(_ => _.DueDate).ToList();
        }

        public IssueModel GetByNumber(string id)
        {
            var all = Get();
            return all.FirstOrDefault(_ => _.Number == id);
        }
    }
}