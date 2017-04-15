using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Niunan.Blog.Web.Controllers
{
    public class TestController : Controller
    {    //用于读取网站静态文件目录
        private IHostingEnvironment hostingEnv;
        DAL.AdminDAL adal;
        DAL.CategoryDAL cadal;
        DAL.BlogDAL blogdal;

        public TestController(DAL.AdminDAL adal, DAL.BlogDAL blogdal, DAL.CategoryDAL cadal, IHostingEnvironment hostingEnv)
        {
            this.adal = adal;
            this.cadal = cadal;
            this.blogdal = blogdal;
            this.hostingEnv = hostingEnv;
        }


        public IActionResult Index()
        {
            string str = "";
            Random r = new Random();

            #region 取IP地址测试
            string ip = "47.88.51.88";
            string ipfile = hostingEnv.WebRootPath + $@"/upload/qqwry.dat";
            str = Tool.GetIPAddress(ip, ipfile);
            #endregion


            #region 添加mysql的测试数据
            //adal.Insert(new Model.Admin() { UserName = "admin", Password = Tool.MD5Hash("admin") });

            //cadal.Insert(new Model.Category() { CaName = "默认分类", Bh = "01", Pbh = "0", });

            //for (int i = 0; i < 22; i++)
            //{
            //    string title = $"第{i}条新闻";
            //    string body = title + " 的内容！";
            //    blogdal.Insert(new Model.Blog()
            //    {
            //        Title = title,
            //        Body = body,
            //        VisitNum = r.Next(0, 1000),
            //        Body_md = "",
            //        CreateDate = DateTime.Now.AddDays(-r.Next(0, 365)),
            //        CaBh = "01",
            //        CaName = "默认分类",
            //        Sort = 0, 
            //    });
            //}
            //str = "添加 MYSQL测试数据成功！！！！"+ DateTime.Now;
            #endregion

            //DAL.BlogDAL blogdal = new DAL.BlogDAL();
            //DAL.CategoryDAL cadal = new DAL.CategoryDAL();
            //DAL.AdminDAL adal = new DAL.AdminDAL();

            //int resid = adal.Insert(new Model.Admin() { UserName = "niunan", Password = "123456789o" });
            //str = "新增admin后返回的值：" + resid;

            //List<Model.Category> list_ca = cadal.GetList("");

            //for (int i = 0; i < 102; i++)
            //{
            //    string title = $"新闻标题{i}";
            //    string body = title + "的内容";
            //    Model.Category ca = list_ca[r.Next(0, list_ca.Count)];
            //    string cabh = ca.Bh,
            //        caname = ca.CaName;

            //    blogdal.Insert(new Model.Blog {  Title = title, Body = body, VisitNum=r.Next(100,999), CaBh = cabh, CaName=caname,
            //     });
            //}
            //str = "添加102条测试新闻成功！";

            //str += "新增的分类返回的ID值 ：" + cadal.Insert(new Model.Category() { CaName = "newca" }) + "<hr />";

            //bool b = cadal.Delete(7);
            //str += "删除ID=7的数据的值 ：" + b + "<hr />";

            //Model.Category ca = cadal.GetModel(8);
            //if (ca != null)
            //{
            //    ca.CaName = "新修改的名称" + DateTime.Now;
            //    bool b2 = cadal.Update(ca);
            //    str += "修改ID=8的实体类的结果 ：" + b2 + "<hr />";
            //}

            //List<Model.Category> list = cadal.GetList("");
            //foreach (var item in list)
            //{
            //    str += $"<div>分类ID：{item.Id}，分类名称：{item.CaName}</div>";
            //}

            return Content(str);
        }

    }
}
