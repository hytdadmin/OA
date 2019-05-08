using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.Common;
/*
Author：liulei
Version：1.0
Date：2013-10-22 14:34:35
Description: DAL层 RePosts
*/
namespace HYTD.BBS.DAL
{

    public class RePostsDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddRePosts(RePosts entity)
        {
            linqHelper.InsertEntity<RePosts>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateRePosts(RePosts entity)
        {
            linqHelper.UpdateEntity<RePosts>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public RePosts GetRePostsEntity(int ID)
        {
            return linqHelper.GetEntity<RePosts>(c => c.ID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteRePosts(int ID)
        {
            linqHelper.DeleteEntity<RePosts>(c => c.ID == ID);
        }

        /// <summary>
        /// 删除多条数据1
        /// </summary>
        /// <param name="fatherId"></param>
        public void DeleteRePostsByFid(int fatherId)
        {
            string sql = "delete from RePosts where fatherId=" + fatherId;
            SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 删除多条数据2
        /// </summary>
        /// <param name="fatherId"></param>
        public void DeleteRePostsByRelFatherId(int relFatherId)
        {
            string sql = "delete from RePosts where ReleaseFatherID=" + relFatherId;
            SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改多条数据状态1
        /// </summary>
        /// <param name="fatherId"></param>
        public void UpdateRePostsStatuByFid(int fatherId, int status)
        {
            string sql = "update RePosts set status=" + status + " where fatherId=" + fatherId;
            SqlHelper.ExecuteSql(sql);
        }

        /// <summary>
        /// 修改多条数据状态2
        /// </summary>
        /// <param name="fatherId"></param>
        public void UpdateRePostsStatusByRelFatherId(int relFatherId, int status)
        {
            string sql = "update RePosts set status=" + status + " where ReleaseFatherID=" + relFatherId;
            SqlHelper.ExecuteSql(sql);
        }


        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<RePosts> GetRePostsList()
        {
            return linqHelper.GetList<RePosts>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetRePostsList(RePostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [RePosts] ";
            string pk = " ID ";
            string fields = " * ";
            string filter = " 1=1 and status=1 ";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.Content))
            {
                filter += string.Format(" and [Content] like '%{0}%' ", StringHelper.SQLFilter(TO.Content));
            }
            if (TO.FatherID != 0)
            {
                filter += string.Format(" and FatherID = '{0}' ", StringHelper.SQLFilter(TO.FatherID.ToString()));
            }

            #endregion

            string sort = " ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "RePostsList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 自定义获得搜索数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetNewTab(RePostsTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            //2013-11-14 liuyh 增加可以按登陆名和姓名查询
            string table = " (select 1 tp,Posts.status,Posts.id IDNew,Posts.id,Posts.CID as CID, Category.Name as Name,Posts.Title as Title, [Description] as remark, ReleaseUser as seuser, reuser='', ReleaseTime as retime,  Posts.UserIP,Posts.isFeedback,isAnonymity,Ispos=1 ";
            table += " ,u.Name senRealName,'' reRealName from Posts left join Category on  Posts.CID=Category.ID  left join [User] u on u.LoginName=Posts.ReleaseUser ";
            //if (!string.IsNullOrEmpty(TO.SendUser))
            //{
            //    table += string.Format(" where u.Name like '%{0}%' ", StringHelper.SQLFilter(TO.SendUser));
            //}
            table += "Union All select 2,RePosts.status,RePosts.id ,FatherID,RePosts.CID as CID, Category.Name as Name,Posts.Title as  Title , Content as remark ,Posts.ReleaseUser ,ReUser as reuser, ReDatetime as retime, RePosts.UserIP,RePosts.isFeedback,RePosts.isAnonymity,Ispos=0  ";
            table += " ,f.Name senRealName ,u.Name reRealName from RePosts left join Category on Category.ID=RePosts.CID  left join Posts on RePosts.FatherID=Posts.ID     left join [User] u on u.LoginName=RePosts.ReUser ";
            //if (!string.IsNullOrEmpty(TO.ReUser))
            //{
            //    table += string.Format(" where  u.name like '%{0}%' ", StringHelper.SQLFilter(TO.ReUser));
            //}
            table += " left join [User] f on f.LoginName=Posts.ReleaseUser ) sbc  ";
            string pk = " Name ";
            string fields = " *,(select top 1 name from [user] where LoginName=seuser) as 'SendUserName', (select top 1 name from [user] where LoginName=reuser) as 'ReUserName' ";
            string filter = " 1=1 ";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.Content))
            {
                filter += string.Format(" and remark like '%{0}%' ", StringHelper.SQLFilter(TO.Content));
            }
            if (TO.CID != 0)
            {
                filter += string.Format(" and CID ={0} ", TO.CID);
            }
            if (!string.IsNullOrEmpty(TO.Title))
            {
                filter += string.Format(" and Title like '%{0}%' ", StringHelper.SQLFilter(TO.Title));
            }
            if (!string.IsNullOrEmpty(TO.UserIP))
            {
                filter += string.Format(" and UserIP like '%{0}%' ", StringHelper.SQLFilter(TO.UserIP));
            }
            //if (!string.IsNullOrEmpty(TO.SendUser) && string.IsNullOrEmpty(TO.ReUser))
            //{
            //    filter += string.Format(" and seuser like '%{0}%' and Ispos=1 ", StringHelper.SQLFilter(TO.SendUser));
            //}
            //else if (!string.IsNullOrEmpty(TO.ReUser) && string.IsNullOrEmpty(TO.SendUser))
            //{
            //    filter += string.Format(" and reuser like '%{0}%' and Ispos=0 ", StringHelper.SQLFilter(TO.ReUser));
            //}
            if (!string.IsNullOrEmpty(TO.ReUser))
            {
                filter += string.Format(" and  (reuser like '%{0}%' or reRealName like '%{0}%')", StringHelper.SQLFilter(TO.ReUser));
            }
            if (!string.IsNullOrEmpty(TO.SendUser))
            {
                filter += string.Format(" and (seuser like '%{0}%' or senRealName like '%{0}%') ", StringHelper.SQLFilter(TO.SendUser));
            }
            if (TO.isAnonymity != 400 && TO.isAnonymity != -1)
            {
                filter += string.Format(" and isAnonymity ={0} ", TO.isAnonymity);
            }
            if (TO.isFeedback != 400 && TO.isFeedback != -1)
            {
                filter += string.Format(" and isFeedback ={0} ", TO.isFeedback);
            }
            if (!string.IsNullOrEmpty(TO.Time))
            {
                filter += string.Format(" and convert(varchar(10),retime,120)>='{0}' ", StringHelper.SQLFilter(TO.Time));
            }
            if (!string.IsNullOrEmpty(TO.EndTime))
            {
                DateTime dtime = DateTime.MaxValue;
                DateTime.TryParse( StringHelper.SQLFilter(TO.EndTime),out dtime);
                filter += string.Format(" and convert(varchar(10),retime,120)<'{0}' ", dtime.AddDays(1).ToString("yyyy-MM-dd"));
            }
            #endregion

            string sort = " retime DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "RePostsList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

    }
}