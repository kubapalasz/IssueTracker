using System.Collections.Generic;

namespace IssueTrackerApi.Core.Services
{
    public interface IIssueRepository
    {
        List<IssueModel> Get();

        void Save(IssueModel issueModel);
    }
}