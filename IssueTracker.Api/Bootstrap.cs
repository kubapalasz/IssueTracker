using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace IssueTracker.Api
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(name: "Default API rout", routeTemplate: "{controller}/{id}", defaults: new
            {
                controller = "Issue",
                id = RouteParameter.Optional
            });
        }
    }
}
