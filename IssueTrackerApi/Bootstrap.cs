using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace IssueTrackerApi
{
    public class Bootstrap
    {
        public void Configure(HttpConfiguration config)
        {
           config.Routes.MapHttpRoute(
               name: "ActionApi",
               routeTemplate: "{controller}/{action}/{id}"
            );

           config.Routes.MapHttpRoute(
               name: "API Default",
               routeTemplate: "{controller}/{id}",
               defaults: new
               {
                   controller = "Issue",
                   id = RouteParameter.Optional
               });

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

        }
    }
}
