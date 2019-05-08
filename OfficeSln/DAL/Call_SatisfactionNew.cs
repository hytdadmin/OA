using System.Collections.Generic;
using DBUtility;
using Models;
using System;
using System.Data.SqlClient;
using System.Data;
using HYTD.Common;
using HYTD.Model.TO;
/*
Author：liulei
Version：1.0
Date：2014-08-12 12:06:39
Description: DAL层 Call_SatisfactionNew
*/
namespace HYTD.DAL
{

	public class Call_SatisfactionNewDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_SatisfactionNew(Call_SatisfactionNew entity)
		{
			linqHelper.InsertEntity<Call_SatisfactionNew>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_SatisfactionNew(Call_SatisfactionNew entity)
		{
			linqHelper.UpdateEntity<Call_SatisfactionNew>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_SatisfactionNew GetCall_SatisfactionNewEntity(int ID)
		{
			return linqHelper.GetEntity<Call_SatisfactionNew>(c => c.CSN_ID == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_SatisfactionNew(int ID)
        {
            linqHelper.DeleteEntity<Call_SatisfactionNew>(c => c.CSN_ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_SatisfactionNew> GetCall_SatisfactionNewList()
		{
			return linqHelper.GetList<Call_SatisfactionNew>();
		}
        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Call_SatisfactionNew> GetCall_SatisfactionNewList(string strIP)
        {
            return linqHelper.GetList<Call_SatisfactionNew>(c=>c.CSN_UserIP.Equals(strIP) && c.CSN_CreateDate.AddHours(1)>DateTime.Now);
        }

        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetCall_SatisfactionNewList(Call_SatisfactionNewTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = @" Call_SatisfactionNew a 
left join Call_Customer on CC_ID=CSN_CC_ID ";
            string pk = " CSN_ID ";
            string fields = " CC_Name,a.* ";
            string filter = "";

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //    filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " CSN_ID ASC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_SatisfactionNewList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
	}
}