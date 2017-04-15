using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Niunan.Blog.Model
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateDate { set; get; }
        /// <summary>
        /// 类型，0后台，1前台
        /// </summary>
        public int Type { set; get; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { set; get; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { set; get; }
        /// <summary>
        /// IP纯真库对应的地址
        /// </summary>
        public string IpAddress { set; get; }
    }
}
