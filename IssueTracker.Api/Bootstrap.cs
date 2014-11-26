using System.Web.Http;

namespace IssueTracker.Api
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "API Default",
                routeTemplate: "{controller}/{id}",
                defaults: new
                {
                    controller = "Issue",
                    id = RouteParameter.Optional
                });
        }
    }
}
