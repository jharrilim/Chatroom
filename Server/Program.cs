using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebSocket.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            CreateWebHostBuilder(args).UseUrls
            (
                env == EnvironmentName.Production? "http://0.0.0.0:80": "http://localhost:80"
            )
            .Build()
            .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
