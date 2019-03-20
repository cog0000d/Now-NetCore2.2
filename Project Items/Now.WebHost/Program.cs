using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Now.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "empleyado";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
