using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Configuration;
using Simple.Data;

namespace IssueTrackerApi
{
    public class IssueController : ApiController
    {
        private static readonly List<IssueModel> Issues = new List<IssueModel>();

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, new IssuesModel {Issues = Issues.ToArray()});
        }

        public HttpResponseMessage Post(IssueModel issue)
        {
            Issues.Add(issue);

            return this.Request.CreateResponse();
        }
    }
}
