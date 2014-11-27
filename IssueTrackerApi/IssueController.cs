using System.Globalization;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace IssueTrackerApi
{
    public class IssueController : ApiController
    {

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new IssuesModel { Issues = FakeDatabase.Issues.ToArray() });
        }

        public HttpResponseMessage Post(IssueModel issue)
        {
            issue.Number = (FakeDatabase.Issues.Count + 1).ToString(CultureInfo.InvariantCulture);

            FakeDatabase.Issues.Add(issue);

            return Request.CreateResponse(HttpStatusCode.OK, issue);
        }
    }
}
