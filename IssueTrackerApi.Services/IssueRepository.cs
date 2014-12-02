using System.Collections.Generic;
using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using ServiceStack.Redis;

namespace IssueTrackerApi.Services
{
    public class IssueRepository : IIssueRepository
    {
        public List<IssueModel> Get()
        {
            using (IRedisClient client = new RedisClient())
            {
                var issueClient = client.GetTypedClient<IssueModel>();

                return issueClient.GetAll().ToList();
            }
        }

        public long Save(IssueModel issueModel)
        {
            using (IRedisClient client = new RedisClient())
            {
                var issueClient = client.GetTypedClient<IssueModel>();

                issueModel.Id = issueClient.GetNextSequence();

                issueClient.Store(issueModel);

                return issueModel.Id;
            }
        }
    }
}
