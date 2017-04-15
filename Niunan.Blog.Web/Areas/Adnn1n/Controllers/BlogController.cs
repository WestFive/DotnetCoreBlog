using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Niunan.Blog.Web.Areas.Controllers
{
    [Area("Adnn1n")]
    public class BlogController : Controller
    {
        DAL.BlogDAL dal;
        DAL.CategoryDAL cadal;

        public BlogController(DAL.BlogDAL bdal, DAL.CategoryDAL cadal) {
            this.dal = bdal;
            this.cadal = cadal;
        }

        public IActionResult Index()
        {
            ViewBag.calist = cadal.GetList("pbh='01'");
            //List<Model.Blog> list = dal.GetList("1=1 order by sort asc,id desc");

            //return View(list);
            return View();
        }

        /// <summary>
        /// 拼接条件
        /// </summary>
        /// <returns></returns>
        public string GetCond(string key, string start, string end, string cabh) {

            string cond = "1=1";
            if (!string.IsNullOrEmpty(key))
            {
                key = Tool.GetSafeSQL(key);
                cond += $" and title like '%{key}%'";
            }
            if (!string.IsNullOrEmpty(start))
            {
                DateTime d;
                if (DateTime.TryParse(start, out d))
                {
                    cond += $" and createdate>='{d.ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(end))
            {
                DateTime d;
                if (DateTime.TryParse(end, out d))
                {
                    cond += $" and createdate<='{d.ToString("yyyy-MM-dd")}'";
                }
            }
            if (!string.IsNullOrEmpty(cabh))
            {
                cabh = Tool.GetSafeSQL(cabh);
                cond += $" and cabh='{cabh}'";
            }
            return cond;
        }

        /// <summary>
        /// 取博客总记录数
        /// </summary>
        /// <returns></returns>
        public IActionResult GetTotalCount(string key, string start, string end, string cabh)
        {
            int totalcount = dal.CalcCount(GetCond(key,start,end,cabh));
            return Content(totalcount.ToString());
        }

        /// <summary>
        /// 取分页数据，返回 JSON
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public IActionResult List(int pageindex, int pagesize, string key, string start, string end, string cabh)
        {
            List<Model.Blog> list = dal.GetList("sort asc,id desc", pagesize, pageindex, GetCond(key, start, end, cabh));
            ArrayList arr = new ArrayList();
            foreach (var item in list)
            {
                arr.Add(new
                {
                    id = item.Id,
                    title = item.Title,
                    createDate = item.CreateDate.ToString("yyyy-MM-dd HH:mm"),
                    visitNum = item.VisitNum,
                    caName = item.CaName,
                    sort = item.Sort,
                });
            }
            return Json(arr);
        }


        public IActionResult Add(int? id)
        {
            ViewBag.calist = cadal.GetList("pbh='01'");
            Model.Blog m = new Model.Blog();
            if (id != null)
            {
                m = dal.GetModel(id.Value);
            }
            return View(m);
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Add(Model.Blog m)
        {
            m.CreateDate = DateTime.Now;
            if(m.CaName==null)
            {
                m.CaName = "默认";
            }
            Model.Category ca = cadal.GetModelByBh(m.CaBh);
            if (ca != null)
            {
                m.CaName = ca.CaName;
            }

            if (m.Id == 0)
            {
                //新增
                dal.Insert(m);
            }
            else
            {
                //修改
                dal.Update(m);
            }
            return Redirect("/Adnn1n/Blog/Index");
        }

        [HttpPost]
        public IActionResult Del(int id)
        {
            bool b = dal.Delete(id);
            if (b)
            {
                return Content("删除成功！");
            }
            else
            {
                return Content("删除失败，请联系管理员！");
            }
        }


    }
}
