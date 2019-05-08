using System.Collections.Generic;
using System.Data;
using HYTD.Model.TO;
using System.Data.SqlClient;
using Models;
using DBUtility;
/*
Author：liulei
Version：1.0
Date：2014-01-10 16:28:32
Description: DAL层 Call_WorkBillHistory
*/
namespace HYTD.DAL
{

	public class Call_WorkBillHistoryDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
				
			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_WorkBillHistory(Call_WorkBillHistory entity)
		{
			linqHelper.InsertEntity<Call_WorkBillHistory>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_WorkBillHistory(Call_WorkBillHistory entity)
		{
			linqHelper.UpdateEntity<Call_WorkBillHistory>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_WorkBillHistory GetCall_WorkBillHistoryEntity(int ID)
		{
			return linqHelper.GetEntity<Call_WorkBillHistory>(c => c.CWBH_ID == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_WorkBillHistory(int ID)
        {
            linqHelper.DeleteEntity<Call_WorkBillHistory>(c => c.CWBH_ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_WorkBillHistory> GetCall_WorkBillHistoryList()
		{
			return linqHelper.GetList<Call_WorkBillHistory>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetCall_WorkBillHistoryList(Call_WorkBillHistoryTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Call_WorkBillHistory] ";
            string pk = " CWBH_ID ";
            string fields = " * ";
            string filter ="";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
			//	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " CWBH_ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_WorkBillHistoryList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}