using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Niunan.Blog.Web.Areas.Controllers
{
    [Area("Adnn1n")]
    public class HomeController : Controller
    {
        //用于读取网站静态文件目录
        private IHostingEnvironment hostingEnv;
       

        public HomeController(IHostingEnvironment env)
        {
            hostingEnv = env;
        }

        public IActionResult Index()
        {
            int? adminid = HttpContext.Session.GetInt32("adminid");
            if (adminid == null)
            {
                //末登录
                return Redirect("/Adnn1n/Login/");
            }
            return View();
        }

        public IActionResult Top() {
            string adminname = HttpContext.Session.GetString("adminusername");
            ViewBag.username = adminname;
            return View(); }

        public IActionResult Left() { return View(); }

        public IActionResult Welcome() { return View(); }

        /// <summary>
        /// layui编辑器里的上传图片功能 
        //{
        //  "code": 0 //0表示成功，其它失败
        //  ,"msg": "" //提示信息 //一般上传失败后返回
        //  ,"data": {
        //    "src": "图片路径"
        //    ,"title": "图片名称" //可选
        //  }
        //}
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ImgUpload()
        {
            var imgFile = Request.Form.Files[0];
            if (imgFile != null && !string.IsNullOrEmpty(imgFile.FileName))
            {
                long size = 0;
                string tempname = "";
                var filename = ContentDispositionHeaderValue
                                .Parse(imgFile.ContentDisposition)
                                .FileName
                                .Trim('"');
                var extname = filename.Substring(filename.LastIndexOf("."), filename.Length - filename.LastIndexOf(".")); //扩展名，如.jpg

                #region 判断后缀
                if (!extname.ToLower().Contains("jpg") && !extname.ToLower().Contains("png") && !extname.ToLower().Contains("gif"))
                {
                    return Json(new { code = 1, msg = "只允许上传jpg,png,gif格式的图片.", });
                }
                #endregion

                #region 判断大小
                long mb = imgFile.Length / 1024 / 1024; // MB
                if (mb > 5)
                {
                    return Json(new { code = 1, msg = "只允许上传小于 5MB 的图片.", });
                }
                #endregion
                 
                var filename1 = System.Guid.NewGuid().ToString().Substring(0, 6) + extname;
                tempname = filename1;
                var path = hostingEnv.WebRootPath; //网站静态文件目录  wwwroot
                string dir = DateTime.Now.ToString("yyyyMMdd");
                //完整物理路径
                string wuli_path = hostingEnv.WebRootPath + $"{Path.DirectorySeparatorChar}upload{Path.DirectorySeparatorChar}{dir}{Path.DirectorySeparatorChar}";
                if (!System.IO.Directory.Exists(wuli_path))
                {
                    System.IO.Directory.CreateDirectory(wuli_path);
                }
                filename = wuli_path + filename1 ;
                size += imgFile.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    imgFile.CopyTo(fs);
                    fs.Flush();
                }
                return Json(new { code = 0, msg = "上传成功", data = new { src = $"/upload/{dir}/{filename1}", title = filename1 } });
            }
            return Json(new { code = 1, msg = "上传失败", });
        }


    }
}
