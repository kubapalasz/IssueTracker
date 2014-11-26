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
    public class JournalController : ApiController
    {


        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse();
        }

        public HttpResponseMessage Post()
        {
            return this.Request.CreateResponse();
        }
    }
}
