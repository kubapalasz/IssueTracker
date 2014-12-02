using System.Collections.Generic;
using System.Linq;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using ServiceStack.Redis;

namespace IssueTrackerApi.Services
{
    public class ProjectRepository : IProjectRepository
    {
        public List<Project> GetAll()
        {
            using (IRedisClient client = new RedisClient())
            {
                var projectClient = client.GetTypedClient<Project>();

                return projectClient.GetAll().ToList();
            }
        }

        public void Save(Project project)
        {
            using (IRedisClient client = new RedisClient())
            {
                var projectClient = client.GetTypedClient<Project>();

                projectClient.Store(project);
            }
        }
    }
}
