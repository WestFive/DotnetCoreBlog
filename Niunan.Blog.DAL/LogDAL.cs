using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Niunan.Blog.Model;

namespace Niunan.Blog.DAL
{
    /// <summary>
    /// 日志表操作类
    /// </summary>
    public class LogDAL
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        public string ConnStr { set; get; }


        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Insert(Model.Log m)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Query<int>(
                    @"insert into log(type,userid,username,remark,ip,ipaddress) values(@type,@userid,@username,@remark,@ip,@ipaddress);SELECT @@IDENTITY;",
                    m).First();
                return resid;
            }
        }

        /// <summary>
        ///分页
        /// </summary> 
        /// <param name="orderstr">如：yydate desc,yytime asc,id desc,必须形成唯一性</param>
        /// <param name="PageSize">每页显示记录数</param>
        /// <param name="PageIndex">页索引</param>
        /// <param name="strWhere">条件，不用加 where</param>
        /// <returns></returns>
        public List<Model.Log> GetList(string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " where " + strWhere;
            }
            // 使用offset,mssql2012以后有用
            string sql = string.Format(
                    "select * from log {0} order by {1} offset {2} rows fetch next {3} rows only",
                    strWhere,
                    orderstr,
                    (PageIndex - 1) * PageSize,
                    PageSize
                );
            // string sql = $"select * from blog {strWhere} order by {orderstr} limit {(PageIndex-1) * PageSize},{PageSize}";
            List<Model.Log> list = new List<Model.Log>();
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                list = connection.Query<Model.Log>(sql).ToList();
            }
            return list;
        }


        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int CalcCount(string cond)
        {
            string sql = "select count(1) from log";
            if (!string.IsNullOrEmpty(cond))
            {
                sql += $" where {cond}";
            }
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {

                int res = connection.ExecuteScalar<int>(sql);
                return res;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {

                int res = connection.Execute(@"delete from log where id = @id", new { id = id });
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
