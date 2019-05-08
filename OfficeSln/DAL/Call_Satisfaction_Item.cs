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
Date：2014-07-16 08:48:00
Description: DAL层 Call_Satisfaction_Item
*/
namespace HYTD.DAL
{

	public class Call_Satisfaction_ItemDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_Satisfaction_Item(Call_Satisfaction_Item entity)
		{
			linqHelper.InsertEntity<Call_Satisfaction_Item>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_Satisfaction_Item(Call_Satisfaction_Item entity)
		{
			linqHelper.UpdateEntity<Call_Satisfaction_Item>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_Satisfaction_Item GetCall_Satisfaction_ItemEntity(int ID)
		{
			return linqHelper.GetEntity<Call_Satisfaction_Item>(c => c.CSI_ID == ID);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
        public Call_Satisfaction_Item GetCall_Satisfaction_ItemEntityBYCSID(int ID)
		{
			return linqHelper.GetEntity<Call_Satisfaction_Item>(c => c.CSI_CS_ID == ID);
		}		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Satisfaction_Item(int ID)
        {
            linqHelper.DeleteEntity<Call_Satisfaction_Item>(c => c.CSI_ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_Satisfaction_Item> GetCall_Satisfaction_ItemList()
		{
			return linqHelper.GetList<Call_Satisfaction_Item>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetCall_Satisfaction_ItemList(Call_Satisfaction_ItemTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Call_Satisfaction_Item] ";
            string pk = " ID ";
            string fields = " * ";
            string filter = "1=1";// string.Format(" JLZT={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //    filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

            #endregion

            string sort = " CSI_ID ASC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_Satisfaction_ItemList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}