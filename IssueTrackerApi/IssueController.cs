using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using IssueTrackerApi.Services;

namespace IssueTrackerApi
{
    public class IssueController : ApiController
    {
        private readonly IIssueService _issueService = new IssueService(new IssueRepository(), new UserRepository(), new ProjectRepository());
        
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new IssuesModel {Issues = _issueService.Get().ToArray()});
        }

        public HttpResponseMessage Get(string id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _issueService.GetByNumber(id));
        }

        public HttpResponseMessage GetByTitle(string id)
        {
            var result = _issueService.Get().Where(_ => _.Title.Contains(id)).ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, new IssuesModel { Issues = result });
        }

        public HttpResponseMessage Post(IssueModel issue)
        {
            var issueId = _issueService.TryAddIssue(issue);

            return issueId == 0
                ? Request.CreateResponse(HttpStatusCode.Forbidden, issue)
                : Request.CreateResponse(HttpStatusCode.OK, _issueService.Get().Single(_ => _.Id == issueId));
        }
    }
}
