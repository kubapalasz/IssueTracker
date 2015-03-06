using System;
using System.Web.Http.SelfHost;

namespace IssueTrackerApi.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = args[0];

            var baseAddress = new Uri(url);
            var config = new HttpSelfHostConfiguration(baseAddress);
            new Bootstrap().Configure(config);

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("WebApi started at: " + url);
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
