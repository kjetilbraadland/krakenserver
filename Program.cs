using System;
using Microsoft.AspNetCore.Hosting;
using aspnetcoreapp;
using System.IO;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder().UseKestrel().UseContentRoot(Directory.GetCurrentDirectory()).UseUrls("http://*:5000").UseStartup<Startup>().Build();

            host.Run();
        }
    }
}
