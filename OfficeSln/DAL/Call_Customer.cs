using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.Model;
using System.Data;
using HYTD.Common;
using HYTD.Model.TO;
using System.Data.SqlClient;
using DBUtility;
using Models;
/*
Author：liulei
Version：1.0
Date：2014-01-10 16:22:15
Description: DAL层 Call_Customer
*/
namespace HYTD.DAL
{

	public class Call_CustomerDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_Customer(Call_Customer entity)
		{
			linqHelper.InsertEntity<Call_Customer>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_Customer(Call_Customer entity)
		{
			linqHelper.UpdateEntity<Call_Customer>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_Customer GetCall_CustomerEntity(int ID)
		{
			return linqHelper.GetEntity<Call_Customer>(c => c.CC_ID == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Customer(int ID)
        {
            linqHelper.DeleteEntity<Call_Customer>(c => c.CC_ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_Customer> GetCall_CustomerList()
		{
			return linqHelper.GetList<Call_Customer>();
		}
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
        public List<Call_Customer> GetCall_CustomerList(string strName)
		{
			return linqHelper.GetList<Call_Customer>(c=>c.CC_Name.IndexOf(strName)>-1);
		}		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetCall_CustomerList(Call_CustomerTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Call_Customer] left join UserInfo on UserCode=CC_Owner";
            string pk = " CC_ID ";
            string fields = " * ";
            string filter ="1=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            if (TO.CC_Type>0)
            {
                filter += string.Format(" and CC_Type = {0} ", TO.CC_Type);
            }

            if (!string.IsNullOrEmpty(TO.CC_Name))
            {
                filter += string.Format(" and CC_Name like '%{0}%' ", StringHelper.SQLFilter(TO.CC_Name));
            }
            if (!string.IsNullOrEmpty(TO.CC_UserName))
            {
                filter += string.Format(" and CC_UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CC_UserName));
            }
            if (!string.IsNullOrEmpty(TO.CC_Owner))
            {
                filter += string.Format(" and UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CC_Owner));
            }
            #endregion

            string sort = " CC_ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_CustomerList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}