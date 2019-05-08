using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.Model;
using System.Data;
using HYTD.Common;
using HYTD.Model.TO;
using System.Data.SqlClient;
using Models;
using DBUtility;
using Models.TO;
/*
Author：youname
Version：1.0
Date：2014-04-25 12:42:36
Description: DAL层 ActiveSuccessfullProductInfo
*/
namespace DAL
{

	public class ActiveSuccessfullProductInfoDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddActiveSuccessfullProductInfo(ActiveSuccessfullProductInfo entity)
		{
			linqHelper.InsertEntity<ActiveSuccessfullProductInfo>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateActiveSuccessfullProductInfo(ActiveSuccessfullProductInfo entity)
		{
			linqHelper.UpdateEntity<ActiveSuccessfullProductInfo>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public ActiveSuccessfullProductInfo GetActiveSuccessfullProductInfoEntity(int ID)
		{
			return linqHelper.GetEntity<ActiveSuccessfullProductInfo>(c => c.ID == ID);
		}
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<ActiveSuccessfullProductInfo> GetActiveSuccessfullProductInfoList()
		{
			return linqHelper.GetList<ActiveSuccessfullProductInfo>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetActiveSuccessfullProductInfoList(ActiveSuccessfullProductInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " ActiveSuccessfullProductInfo a left join (select Code,CName from ActiveProduct) c on c.Code=a.ProductID left join Call_Customer b on a.CustomerID=b.CC_ID ";
            string pk = " ID ";
            string fields = " * ";
            string filter = "1=1";

            #region 组织查询条件

            if (!string.IsNullOrEmpty(TO.CustomerName))
            {
                filter += string.Format(" and CC_Name like '%{0}%' ", StringHelper.SQLFilter(TO.CustomerName));
            }

            if (!string.IsNullOrEmpty(TO.LoginName))
            {
                filter += string.Format(" and LoginName like '%{0}%' ", StringHelper.SQLFilter(TO.LoginName));
            }
            if (!string.IsNullOrEmpty(TO.StartTime))
            {
                DateTime dtStart = DateTime.Now;
                DateTime.TryParse(TO.StartTime, out dtStart);
                dtStart = dtStart.AddDays(-1);
                filter += string.Format(" and CreateDate >'{0}' ", dtStart.ToShortDateString());
            }
            if (!string.IsNullOrEmpty(TO.EndTime))
            {
                DateTime dtEnd = DateTime.Now;
                DateTime.TryParse(TO.EndTime, out dtEnd);
                dtEnd = dtEnd.AddDays(1);
                filter += string.Format(" and CreateDate < '{0}' ", dtEnd.ToShortDateString());
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "ActiveSuccessfullProductInfoList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}