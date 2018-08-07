using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebSocket.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string env = ParseArgs(args);
            string url = env == "Development" ? "http://localhost:80" : "http://0.0.0.0:80"; 
            Console.WriteLine(env);
            CreateWebHostBuilder(args)
                .UseUrls(url)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static string ParseArgs(string[] args) 
        {
            for(int i = 0; i < args.Length; i++)
            {
                if(args[i] == "--environment")
                {
                    Console.WriteLine(args[i+1]);
                    return args[i+1];
                }
            }
            return "Development";
        }
    }
}
