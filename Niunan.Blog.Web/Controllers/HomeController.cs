using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Niunan.Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        //用于读取网站静态文件目录
        private IHostingEnvironment hostingEnv;

        DAL.CategoryDAL cadal;
        DAL.BlogDAL bdal;
        DAL.LogDAL logdal;

        public HomeController(DAL.CategoryDAL cadal, DAL.BlogDAL bdal, DAL.LogDAL logdal, IHostingEnvironment hostingEnv)
        {
            this.cadal = cadal;
            this.bdal = bdal;
            this.logdal = logdal;
            this.hostingEnv = hostingEnv;
        }


        public IActionResult Index(string key, string month, string cabh)
        {
            ViewBag.blogdal = bdal;

            ViewBag.calist = cadal.GetList("pbh='01'");
            ViewBag.blogmonth = bdal.GetBlogMonth();

            ViewBag.search_key = key;
            ViewBag.search_month = month;
            ViewBag.search_cabh = cabh;

            string ip1 = HttpContext.Request.Headers["X-Real-IP"]; //取IP，NGINX中的配置里要写上

            //var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
            //string ip2 = feature.RemoteIpAddress.ToString();

            //string ip3 = HttpContext.Request.Headers["X-Forwarded-For"];

            //string all_header = "";
            //foreach (var item in HttpContext.Request.Headers.Keys)
            //{
            //    all_header += item + " → "+ HttpContext.Request.Headers[item];
            //}

            
            
            string ipfile = hostingEnv.WebRootPath + $"{Path.DirectorySeparatorChar}upload{Path.DirectorySeparatorChar}qqwry.dat"; //IP纯真库物理路径
            string ipaddress = string.IsNullOrEmpty(ip1)?"":  Tool.GetIPAddress(ip1, ipfile);  //查IP纯真库
            logdal.Insert(new Model.Log
            {
                Type = 1,
                UserId = 0,
                UserName = "访客",
                Remark = "访客登录 " ,
                Ip = ip1,
                IpAddress = ipaddress
            });

            return View();
        }

    }
}
