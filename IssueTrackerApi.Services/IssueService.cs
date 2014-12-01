using System;
using System.Collections.Generic;
using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;

namespace IssueTrackerApi.Services
{
    public class IssueService : IIssueService
    {
        public IIssueRepository _issueRepository { get; set; }
        public IUserRepository _userRepository { get; set; }

        public IssueService(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
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

        public IEnumerable<IssueModel> GetByName(string projectName)
        {
            var all = Get();
            return all.Where(_ => _.Project.Name == projectName);
        }

        public void AssignUser(IssueModel sampleIssue, string userLogin)
        {
            var user = _userRepository.GetByLogin(userLogin);
            if(user == null)
                throw new Exception("User doesn't exist");
            sampleIssue.AssignedTo = user;
            _issueRepository.Save(sampleIssue);
        }
    }
}