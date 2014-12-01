using System.Globalization;
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
        private readonly IIssueService _issueService = new IssueService(new IssueRepository(), new UserRepository());
        
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
            var result = FakeDatabase.Issues.Where(_ => _.Title.Contains(id)).ToArray();

            return Request.CreateResponse(HttpStatusCode.OK, new IssuesModel { Issues = result });
        }

        public HttpResponseMessage Post(IssueModel issue)
        {
            if (FakeDatabase.Projects.All(_ => _.Name != issue.Project.Name))
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, issue);
            }

            issue.Number = (FakeDatabase.Issues.Count + 1).ToString(CultureInfo.InvariantCulture);

            FakeDatabase.Issues.Add(issue);

            return Request.CreateResponse(HttpStatusCode.OK, issue);
        }
    }
}
