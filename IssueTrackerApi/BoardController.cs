using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace IssueTrackerApi
{
    public class BoardController : ApiController
    {

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK,
                new IssuesModel {Issues = FakeDatabase.Issues.OrderBy(_ => _.DueDate).ToArray()});
        }
    }
}
