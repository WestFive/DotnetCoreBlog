using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Niunan.Blog.DAL
{
    /// <summary>
    /// /博客表的数据库操作类
    /// </summary>
    public class BlogDAL
    {
        /// <summary>
        /// 数据库连接字符串，从web层传入
        /// </summary>
        public string ConnStr { set; get; }

        /// <summary>
        /// 获取有博客的月份
        /// </summary>
        /// <returns></returns>
        public List<string> GetBlogMonth()
        {
            // mssql 
             string sql = "select left( CONVERT(varchar(100), CreateDate, 23),7) as aaa from blog group by left( CONVERT(varchar(100), CreateDate, 23),7) order by aaa desc";
            //mysql
          //  string sql = "    select DATE_FORMAT(createdate,'%Y-%m') as aaa from blog group by DATE_FORMAT(createdate, '%Y-%m') order by aaa desc";
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                var list = connection.Query<string>(sql).ToList();
                return list;
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
        public List<Model.Blog> GetList(string orderstr, int PageSize, int PageIndex, string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " where " + strWhere;
            }
            // 使用offset,mssql2012以后有用
            string sql = string.Format(
                    "select * from blog {0} order by {1} offset {2} rows fetch next {3} rows only",
                    strWhere,
                    orderstr,
                    (PageIndex - 1) * PageSize,
                    PageSize
                );
            // string sql = $"select * from blog {strWhere} order by {orderstr} limit {(PageIndex-1) * PageSize},{PageSize}";
            List<Model.Blog> list = new List<Model.Blog>();
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                list = connection.Query<Model.Blog>(sql).ToList();
            }
            return list;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public int Insert(Model.Blog m)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                int resid = connection.Query<int>(
                    @"INSERT INTO Blog
                       ( Title
                       ,CreateDate
                       ,Body
                       ,Body_md
                       ,VisitNum
                       ,CaBh
                       ,CaName
                       ,Remark
                       ,Sort)
                 VALUES
                       ( @Title
                       ,@CreateDate
                       ,@Body
                       ,@Body_md
                       ,@VisitNum
                       ,@CaBh
                       ,@CaName
                       ,@Remark
                       ,@Sort);SELECT @@IDENTITY;",
                    m).First();
                return resid;
            }
        }

        /// <summary>
        /// 计算记录数
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public int CalcCount(string cond)
        {
            string sql = "select count(1) from blog";
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

                int res = connection.Execute(@"delete from Blog where id = @id", new { id = id });
                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public List<Model.Blog> GetList(string cond)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {
                string sql = "select * from Blog";
                if (!string.IsNullOrEmpty(cond))
                {
                    sql += $" where {cond}";
                }
                var list = connection.Query<Model.Blog>(sql).ToList();
                return list;

            }
        }

        /// <summary>
        /// 获取实体类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.Blog GetModel(int id)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {

                var m = connection.Query<Model.Blog>("select * from Blog where id=@id",

                  new { id = id }).FirstOrDefault();
                return m;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool Update(Model.Blog m)
        {
            using (var connection = ConnectionFactory.GetOpenConnection(ConnStr))
            {

                int res = connection.Execute(@"UPDATE  Blog
                                                       SET  Title = @Title
                                                          ,Body = @Body
                                                          ,Body_md = @body_md
                                                          ,VisitNum =@VisitNum
                                                          ,CaBh = @CaBh
                                                          ,CaName = @CaName
                                                          ,Remark =@Remark
                                                          ,Sort = @Sort
                                                     WHERE Id=@Id", m);

                if (res > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
