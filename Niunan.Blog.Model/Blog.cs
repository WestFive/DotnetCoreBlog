using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Niunan.Blog.Model
{
    /// <summary>
    /// 博客表
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 内容 
        /// </summary>
        public string Body { set; get; }
        /// <summary>
        /// 内容-markdown
        /// </summary>
        public string Body_md { set; get; }
        /// <summary>
        /// 访问量
        /// </summary>
        public int VisitNum { set; get; }
        /// <summary>
        /// 分类 编号
        /// </summary>
        public string CaBh { set; get; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CaName { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { set; get; }
    }
}
