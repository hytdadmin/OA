using System.Collections.Generic;
using System.Data;
using HYTD.Model.TO;
using System.Data.SqlClient;
using Models;
using DBUtility;
using HYTD.Common;
using System;
using System.Configuration;
/*
Author：liulei
Version：1.0
Date：2014-01-10 16:22:19
Description: DAL层 Call_WorkBill
*/
namespace HYTD.DAL
{

	public class Call_WorkBillDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddCall_WorkBill(Call_WorkBill entity)
		{
			linqHelper.InsertEntity<Call_WorkBill>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateCall_WorkBill(Call_WorkBill entity)
		{
			linqHelper.UpdateEntity<Call_WorkBill>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Call_WorkBill GetCall_WorkBillEntity(int ID)
		{
			return linqHelper.GetEntity<Call_WorkBill>(c => c.CWB_ID == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_WorkBill(int ID)
        {
            linqHelper.DeleteEntity<Call_WorkBill>(c => c.CWB_ID == ID);
        }
		
			
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
        public List<Call_WorkBill> GetCall_WorkBillList(int intCCID, string strProductID, string strLoginName)
		{
            return linqHelper.GetList<Call_WorkBill>(c => c.CWB_CCID == intCCID 
                && c.CWB_ProductID.ToLower().Equals(strProductID.ToLower()) 
                && c.CWB_CallInLoginName.ToLower().Equals(strLoginName.ToLower()));
		}	
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Call_WorkBill> GetCall_WorkBillList()
		{
			return linqHelper.GetList<Call_WorkBill>();
		}

        /// <summary>
        /// 获取实体分页 
        /// </summary>
        public int GetCall_WorkBillCounts(int intCusID)
        {
            int intCounts = 0;
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@CCID",SqlDbType.Int)
            };
            parameters[0].Value = intCusID;

            DataSet ds = SqlHelper.RunProcedure("UP_Call_getWorkBillCountsByCCID", parameters, "Call_WorkBillList");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               int.TryParse(ds.Tables[0].Rows[0][0].ToString(),out intCounts);
            }
            return intCounts;
        }
        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetCall_WorkBillList(string strWhere, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = @" dbo.Call_Customer b 
left join Call_WorkBill a on a.CWB_CCID=b.CC_ID
left join dbo.[UserInfo] c1 on c1.UserCode=b.CC_Owner
left join dbo.[UserInfo] c on c.UserCode=a.CWB_Creater
left join dbo.[UserInfo] d on d.UserCode=a.CWB_ForUser 
left join Call_Category e on e.C_ID=a.CWB_Type and e.c_type=1
left join Call_Category f on f.C_ID=a.CWB_Status and f.c_type=2
left join Call_Category g on g.C_ID=a.CWB_MYDStatus and g.c_type=4";
            string pk = " CWB_ID ";
            string fields = @" CWB_Code,CWB_ID,CWB_CCID,CC_ID,CC_Name,CC_UserName,CC_Tel,CWB_Type,CWB_Creater,CWB_CreateTime,
CWB_ForUser,CWB_OperationTime,c.UserName CreateUserName,d.UserName ServiceUserName,f.C_Name CWB_Status,e.C_Name,a.CWB_MYDStatus,g.C_Name MYDStatus,CWB_CallInUserName,CWB_CallInTel ";
            string filter = "1=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件
             filter = strWhere;          
            #endregion

             string sort = " CWB_MYDStatus,CWB_ID desc ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_WorkBillList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetCall_WorkBillList(Call_WorkBillTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = @" dbo.Call_Customer b 
left join Call_WorkBill a on a.CWB_CCID=b.CC_ID
left join dbo.[UserInfo] c1 on c1.UserCode=b.CC_Owner
left join dbo.[UserInfo] c on c.UserCode=a.CWB_Creater
left join dbo.[UserInfo] d on d.UserCode=a.CWB_ForUser 
left join Call_Category e on e.C_ID=a.CWB_Type and e.c_type=1
left join Call_Category f on f.C_ID=a.CWB_Status and f.c_type=2
left join Call_Category g on g.C_ID=a.CWB_MYDStatus and g.c_type=4";
            string pk = " CWB_ID ";
            string fields = @" CWB_Code,CWB_ID,CWB_CCID,CC_ID,CC_Name,CC_UserName,CC_Tel,CWB_Type,CWB_Creater,CWB_CreateTime,
CWB_ForUser,CWB_OperationTime,c.UserName CreateUserName,d.UserName ServiceUserName,f.C_Name CWB_Status,e.C_Name,g.C_Name MYDStatus,CWB_CallInUserName,CWB_CallInTel ";
            string filter = "1=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件
            if (TO.CWB_Type>0)
            {
                filter += string.Format(" and CWB_Type = {0} ", TO.CWB_Type);
            }
            if (!string.IsNullOrEmpty(TO.CWB_Description))
            {
                filter += string.Format(" and c1.UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CWB_Description));
            }
            if (!string.IsNullOrEmpty(TO.CWB_Solution))
            {
                filter += string.Format(" and d.UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CWB_Solution));
            }
            //if (!string.IsNullOrEmpty(TO.CWB_Solution))
            //{
            //    filter += string.Format(" and d.UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CWB_Solution));
            //}
            if (!string.IsNullOrEmpty(TO.CWB_Remark))
            {
                filter += string.Format(" and CC_Name like '%{0}%' ", StringHelper.SQLFilter(TO.CWB_Remark));
            }
            if (!string.IsNullOrEmpty(TO.CWB_Code))
            {
                filter += string.Format(" and CWB_Code like '%{0}%' ", StringHelper.SQLFilter(TO.CWB_Code));
            }
            if (TO.CWB_CreateTime > Convert.ToDateTime("1979-01-01"))
            {
                filter += string.Format(" and CWB_CreateTime >= '{0}' ", TO.CWB_CreateTime.ToString("yyyy-MM-dd"));
            }
            if (TO.CWB_OperationTime>Convert.ToDateTime("1979-01-01"))
            {
                filter += string.Format(" and CWB_CreateTime < '{0}' ", TO.CWB_OperationTime.AddDays(1).ToString("yyyy-MM-dd"));
            }
            int intStatus = 27;
            int.TryParse(ConfigurationManager.AppSettings["workbillStatusAll"].ToString(), out intStatus);
            if (TO.CWB_Status == intStatus)
            {

            }
            else
            {
                filter += string.Format(" and a.CWB_Status = {0} ", TO.CWB_Status);
            }
            #endregion

            string sort = " isnull(CWB_Status,120),CWB_ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_WorkBillList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 生成工单编号
        /// </summary>
        /// <returns></returns>
        public string WorkBillCreateCode(string strDate)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@date",SqlDbType.VarChar,10),
                new SqlParameter("@output",SqlDbType.VarChar,100)
            };
            parameters[0].Value = strDate;
            parameters[1].Direction = ParameterDirection.Output;

            DataSet ds = SqlHelper.RunProcedure("UP_WorkBill_CreateCode", parameters, "Call_WorkBillList");
            strResult = (string)parameters[1].Value;

            return strResult;
        }



        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        public DataSet ImportExcelCustomerWorkBill(string strWhere)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@strWhere",SqlDbType.VarChar,500)
            };
            parameters[0].Value = strWhere;

            DataSet ds = SqlHelper.RunProcedure("UP_import_CustomerWorkBillNew", parameters, "Call_WorkBillList");

            return ds;
        }
        public DataSet getInfoForCounteByUser(string strWhere)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@UserCode",SqlDbType.VarChar,500)
            };
            parameters[0].Value = strWhere;

            DataSet ds = SqlHelper.RunProcedure("UP_Report_UserInfos", parameters, "Call_WorkBillList");

            return ds;
        }
        public DataSet getInfoForCounteByALL(string strWhere)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@strWhere",SqlDbType.VarChar,500)
            };
            parameters[0].Value = strWhere;

            DataSet ds = SqlHelper.RunProcedure("UP_Report_WorkBillAllCounts", parameters, "Call_WorkBillList");

            return ds;
        }
        public DataSet getInfoForCounteByMonth(DateTime dtDateTime)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@DateTime",SqlDbType.DateTime)
            };
            parameters[0].Value = dtDateTime;

            DataSet ds = SqlHelper.RunProcedure("UP_Report_MonthTotalCounts", parameters, "Call_WorkBillList");

            return ds;
        }
        public DataSet getInfoForCounteBySelectedYear(DateTime dtDateTime)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@DateTime",SqlDbType.DateTime)
            };
            parameters[0].Value = dtDateTime;

            DataSet ds = SqlHelper.RunProcedure("UP_Report_YearTotalCounts", parameters, "Call_WorkBillList");

            return ds;
        }
        public DataSet getInfoForCounteBySelectedYearTime(DateTime dtDateTime)
        {
            string strResult = string.Empty;
            SqlParameter[] parameters = {
                new SqlParameter("@DateTime",SqlDbType.DateTime)
            };
            parameters[0].Value = dtDateTime;

            DataSet ds = SqlHelper.RunProcedure("UP_Report_YearTimeTotalCounts", parameters, "Call_WorkBillList");

            return ds;
        }

	}
}