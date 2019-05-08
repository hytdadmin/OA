using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.Model;
using System.Data;
using HYTD.Common;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.Models.TO;

namespace HYTD.DAL
{
    public class Call_FAQListDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddCall_FAQList(Call_FAQList entity)
        {
            linqHelper.InsertEntity<Call_FAQList>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateCall_FAQList(Call_FAQList entity)
        {
            linqHelper.UpdateEntity<Call_FAQList>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public Call_FAQList GetCall_FAQListEntity(int ID)
        {
            return linqHelper.GetEntity<Call_FAQList>(c => c.CF_ID== ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_FAQList(int ID)
        {
            linqHelper.DeleteEntity<Call_FAQList>(c => c.CF_ID == ID);
        }



        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Call_FAQList> GetCall_FAQList()
        {
            return linqHelper.GetList<Call_FAQList>();
        }

        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Call_FAQList> GetCall_FAQList(string strName)
        {
            return linqHelper.GetList<Call_FAQList>(c => c.CF_Describe.IndexOf(strName) > -1);
        }

        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetCall_FAQList(Call_FAQListTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Call_FAQList] ";
            string pk = " CF_ID ";
            string fields = " * ";
            string filter = "1=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            if (TO.CF_SoftTypeID > 0)
            {
                filter += string.Format(" and CF_SoftTypeID = {0} ", TO.CF_SoftTypeID);
            }

            if (!string.IsNullOrEmpty(TO.CF_ErrorList))
            {
                filter += string.Format(" and CF_ErrorList like '%{0}%' ", StringHelper.SQLFilter(TO.CF_ErrorList));
            }
            if (TO.CF_AddDate > Convert.ToDateTime("1980-01-01"))
            {
                filter += string.Format(" and CF_AddDate >= '{0}' ", TO.CF_AddDate.ToString("yyyy-MM-dd"));
            }
            if (TO.CF_EndDate > Convert.ToDateTime("1980-01-01"))
            {
                filter += string.Format(" and CF_AddDate < '{0}' ", TO.CF_EndDate.AddDays(1).ToString("yyyy-MM-dd"));
            }
            #endregion

            string sort = " CF_ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_FAQList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
    }
}
