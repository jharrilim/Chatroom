using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebSocket.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string env = GetEnvironment(args);
            string url = env == "Development" ? "http://localhost:80" : "http://0.0.0.0:80"; 
            CreateWebHostBuilder(args)
                .UseUrls(url)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => 
                {
                    if(GetEnvironment(args) != "Development")
                        options.Listen(IPAddress.Any, 443, listenOpts =>
                        {
                            listenOpts.UseHttps("/etc/ca-certificates/application.pfx", "ASafePassword");
                        });
                }).UseStartup<Startup>();

        private static string GetEnvironment(string[] args) 
        {
            for(int i = 0; i < args.Length; i++)
            {
                if(args[i] == "--environment")
                {
                    return args[i+1];
                }
            }
            return "Development";
        }
    }
}
