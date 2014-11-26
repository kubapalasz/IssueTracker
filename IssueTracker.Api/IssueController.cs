using System.Net.Http;
using System.Web.Http;

namespace IssueTracker.Api
{
    public class IssueController: ApiController
    {
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse();
        }
    }
}
