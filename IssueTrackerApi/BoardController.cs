using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace IssueTrackerApi
{
    public class BoardController : ApiController
    {
        private readonly IIssueService _issueService = new IssueService(new IssueRepository());

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                new IssuesModel {Issues = _issueService.GetOrderedByDueDateAsc().ToArray()});
        }
    }
}
