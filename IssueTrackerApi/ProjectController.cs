using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using IssueTrackerApi.Core;
using IssueTrackerApi.Services;

namespace IssueTrackerApi
{
    public class ProjectController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new IssuesModel { Issues = FakeDatabase.Issues.ToArray() });
        }

        public HttpResponseMessage Post(Project project)
        {
            var existingProject = FakeDatabase.Projects.FirstOrDefault(_ => _.Name == project.Name);

            if (existingProject == null)
            {
                FakeDatabase.Projects.Add(project);
                return Request.CreateResponse(HttpStatusCode.OK, project);
                
            }
            return Request.CreateResponse(HttpStatusCode.Forbidden, project);
        }
    }
}
