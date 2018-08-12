using System;
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
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var b = WebHost.CreateDefaultBuilder(args);
#if RELEASE
            b.UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 443, listenOpts =>
                        {
                            listenOpts.UseHttps(Environment.GetEnvironmentVariable("certpath"), Environment.GetEnvironmentVariable("certpass"));
                        });
                });
#endif
            return b.UseStartup<Startup>();
        }
    }
}
