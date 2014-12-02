using System.Web.Http;
using System.Net.Http;
using System.Net;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using IssueTrackerApi.Services;

namespace IssueTrackerApi
{
    public class ProjectController : ApiController
    {
        private readonly IIssueService _issueService = new IssueService(new IssueRepository(), new UserRepository(), new ProjectRepository());
        private readonly IProjectService _projectService = new ProjectService(new ProjectRepository());

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                new IssuesModel {Issues = _issueService.GetOrderedByDueDateAsc().ToArray()});
        }

        public HttpResponseMessage Post(Project project)
        {
            return
                Request.CreateResponse(
                    _projectService.TryAddProject(project) ? HttpStatusCode.OK : HttpStatusCode.Forbidden, project);
        }
    }
}
