using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBUtility;
using System.Data.SqlClient;
using Models.TO;
using Models;

namespace DAL
{
    public class FeedbackDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddFeedback(Feedback entity)
        {
            linqHelper.InsertEntity<Feedback>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateFeedback(Feedback entity)
        {
            linqHelper.UpdateEntity<Feedback>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public Feedback GetFeedbackEntity(int ID)
        {
            return linqHelper.GetEntity<Feedback>(c => c.ID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFeedback(int ID)
        {
            linqHelper.DeleteEntity<Feedback>(c => c.ID == ID);
        }



        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Feedback> GetFeedbackList()
        {
            return linqHelper.GetList<Feedback>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetFeedbackList(FeedbackTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Feedback] ";
            string pk = " FID ";
            string fields = " * ";
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " FID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "FeedbackList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }



    }
}
