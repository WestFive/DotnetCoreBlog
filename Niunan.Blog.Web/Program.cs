using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Niunan.Blog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string arg1 = args.Length > 0 ? args[0] : "http://localhost:5000";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(arg1)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            Console.Write("传入的第一个参数："+arg1+"\r\n");

            host.Run();

           
        }
    }
}
