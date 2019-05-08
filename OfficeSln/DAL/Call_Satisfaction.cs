using System.Collections.Generic;
using System.Data;
using HYTD.Common;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.Model.TO;
/*
Author：liulei
Version：1.0
Date：2014-07-16 08:47:57
Description: DAL层 Call_Satisfaction
*/
namespace HYTD.DAL
{

	public class Call_SatisfactionDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_Satisfaction(Call_Satisfaction entity)
		{
			linqHelper.InsertEntity<Call_Satisfaction>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_Satisfaction(Call_Satisfaction entity)
		{
			linqHelper.UpdateEntity<Call_Satisfaction>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_Satisfaction GetCall_SatisfactionEntity(int ID)
		{
			return linqHelper.GetEntity<Call_Satisfaction>(c => c.CS_ID == ID);
		}
		
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
        public Call_Satisfaction GetCall_SatisfactionEntityBYCWBID(int ID)
		{
			return linqHelper.GetEntity<Call_Satisfaction>(c => c.CS_CWB_ID == ID);
		}
				
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Satisfaction(int ID)
        {
            linqHelper.DeleteEntity<Call_Satisfaction>(c => c.CS_ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_Satisfaction> GetCall_SatisfactionList()
		{
			return linqHelper.GetList<Call_Satisfaction>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetCall_SatisfactionList(Call_SatisfactionTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Call_Satisfaction] ";
            string pk = " CS_ID ";
            string fields = " * ";
            string filter = "1=1";// string.Format(" JLZT={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //    filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " MC ASC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_SatisfactionList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}