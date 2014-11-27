using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace IssueTrackerApi
{
    public class ProjectController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new IssuesModel { Issues = FakeDatabase.Issues.ToArray() });
        }

        public HttpResponseMessage Post(ProjectModel project)
        {
            FakeDatabase.Projects.Add(project);

            return Request.CreateResponse(HttpStatusCode.OK, project);
        }
    }
}
