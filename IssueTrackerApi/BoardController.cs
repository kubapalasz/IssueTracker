using System.Web.Http;
using System.Net.Http;
using System.Net;
using IssueTrackerApi.Core;
using IssueTrackerApi.Core.Services;
using IssueTrackerApi.Services;

namespace IssueTrackerApi
{
    public class BoardController : ApiController
    {
        private readonly IIssueService _issueService = new IssueService(new IssueRepository(), new UserRepository());

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                new IssuesModel {Issues = _issueService.GetOrderedByDueDateAsc().ToArray()});
        }
    }
}
