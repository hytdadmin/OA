using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.BBS.Model.TO;

namespace HYTD.BBS.DAL
{

    public class PostsDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddPosts(Posts entity)
        {
            linqHelper.InsertEntity<Posts>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdatePosts(Posts entity)
        {
            linqHelper.UpdateEntity<Posts>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public Posts GetPostsEntity(int ID)
        {
            return linqHelper.GetEntity<Posts>(c => c.ID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletePosts(int ID)
        {
            linqHelper.DeleteEntity<Posts>(c => c.ID == ID);
        }


        /// <summary>
        /// 计算次数
        /// </summary>
        /// <param name="userName"></param>
        public void CountPosts(string userName)
        {
            SqlParameter[] parameters ={
                    new SqlParameter("@username",SqlDbType.VarChar,50)
            };
            parameters[0].Value = userName;
            SqlHelper.RunProcedure("SP_CountPosts", parameters);
        }

        /// <summary>
        /// 计算回帖数
        /// </summary>
        /// <param name="userName"></param>
        public void CountRePosts(int fatherId)
        {
            SqlParameter[] parameters ={
                    new SqlParameter("@fatherId",SqlDbType.Int)
            };
            parameters[0].Value = fatherId;
            SqlHelper.RunProcedure("SP_CountRePostsNew", parameters);
        }




        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Posts> GetPostsList()
        {



            return linqHelper.GetList<Posts>();

        }
        public DataTable GetPostsInfoList()
        {
            string strsql = @"select p.*,u.Name name ,c.Name Cname,LastUser lastusername
                                from 
                                Posts P left join [User] u on p.ReleaseUser=u.LoginName 
                                left join Category c on p.CID=c.ID where c.satatus=1 ";
            DataTable dt = SqlHelper.Query(strsql).Tables[0];
            return dt;
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetPostsList(PostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = @"Posts P left join [User] u on p.ReleaseUser=u.LoginName 
                                left join Category c on p.CID=c.ID";
            string pk = " P.ID ";
            string fields = " p.*,u.Name name ,c.Name Cname,LastUser lastusername";
            string filter = " 1=1 and status=1 and c.satatus=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件

            if (!string.IsNullOrEmpty(TO.HotCount))
            {
                filter += string.Format(" and P.ID in (select FatherID from RePosts group by FatherID having COUNT(FatherID)>{0})", TO.HotCount);
            }
            if (!string.IsNullOrEmpty(TO.isback))
            {

                filter += string.Format(" and p.isFeedback={0}", TO.isback);
            }
            if (!string.IsNullOrEmpty(TO.ReleaseUser))
            {
                filter += string.Format(" and p.ReleaseUser='{0}'", TO.ReleaseUser);
            }
            if (TO.CID != 0)
            {
                filter += string.Format(" and p.cid={0}", TO.CID);
            }
            if (!string.IsNullOrEmpty(TO.Title))
            {
                filter += string.Format(" and p.title like '%{0}%'", TO.Title);
            }
            if (TO.isFeedback == 1)
            {
                filter += string.Format(" and p.isFeedback=1");
            }
            if (TO.ReleaseTime > Convert.ToDateTime("1990-01-01"))
            {
                filter += string.Format(" and p.ReleaseTime>='{0}'", TO.ReleaseTime);
            }
            #endregion

            string sort = " p.ReleaseTime DESC ";//排序
            if (!string.IsNullOrEmpty(orderBy))
                sort = orderBy;

            SqlParameter[] parameters = {
                new SqlParameter("@Tables",SqlDbType.VarChar,1000),
                new SqlParameter("@PK",SqlDbType.VarChar,100),
                new SqlParameter("@Fields",SqlDbType.VarChar,1000),
                new SqlParameter("@Pageindex",SqlDbType.Int),
                new SqlParameter("@PageSize",SqlDbType.Int),
                new SqlParameter("@Filter",SqlDbType.VarChar,1000),
                new SqlParameter("@Sort",SqlDbType.VarChar,200),
                new SqlParameter("@RowCount",SqlDbType.Int)
            };
            parameters[0].Value = table;
            parameters[1].Value = pk;
            parameters[2].Value = fields;
            parameters[3].Value = pageIndex;
            parameters[4].Value = pageSize;
            parameters[5].Value = filter;
            parameters[6].Value = sort;
            parameters[7].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "PostsList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }



    }
}