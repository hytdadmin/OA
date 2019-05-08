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
/*
Author：liulei
Version：1.0
Date：2014-02-08 15:49:39
Description: DAL层 Call_VisitBill
*/
namespace HYTD.DAL
{

    public class Call_VisitBillDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddCall_VisitBill(Call_VisitBill entity)
        {
            linqHelper.InsertEntity<Call_VisitBill>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateCall_VisitBill(Call_VisitBill entity)
        {
            linqHelper.UpdateEntity<Call_VisitBill>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public Call_VisitBill GetCall_VisitBillEntity(int ID)
        {
            return linqHelper.GetEntity<Call_VisitBill>(c => c.CVB_ID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_VisitBill(int ID)
        {
            linqHelper.DeleteEntity<Call_VisitBill>(c => c.CVB_ID == ID);
        }



        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<Call_VisitBill> GetCall_VisitBillList()
        {
            return linqHelper.GetList<Call_VisitBill>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetCall_VisitBillList(Call_VisitBillTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            //string table = " [Call_VisitBill] ";
            //string pk = " CVB_ID ";
            //string fields = " * ";
            //string filter ="";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            string table = @" Call_VisitBill a 
left join dbo.Call_Customer b on a.CVB_CCID=b.CC_ID
left join dbo.[UserInfo] c1 on c1.UserCode=b.CC_Owner
left join dbo.[UserInfo] c on c.UserCode=a.CVB_Creater
left join dbo.[UserInfo] d on d.UserCode=a.CVB_ForUser 
left join Call_Category e on e.C_ID=a.CVB_Type and e.c_type=1
left join Call_Category f on f.C_ID=a.CVB_Status and f.c_type=3";
            string pk = " CVB_ID ";
            string fields = @" CVB_ID,CVB_CCID,CVB_CWB_ID,CC_ID,CC_Name,CC_UserName,CC_Tel,CVB_Type,CVB_Creater,CVB_CreateTime,
CVB_ForUser,CVB_OperationTime,c.UserName CreateUserName,d.UserName ServiceUserName,f.C_Name CVB_Status,e.C_Name ";
            string filter = "1=1";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件

            if (TO.CVB_Type > 0)
            {
                filter += string.Format(" and CVB_Type = {0} ", TO.CVB_Type);
            }
            if (!string.IsNullOrEmpty(TO.CVB_Solution))
            {
                filter += string.Format(" and c.UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CVB_Solution));
            }
            if (!string.IsNullOrEmpty(TO.CVB_Description))
            {
                filter += string.Format(" and c1.UserName like '%{0}%' ", StringHelper.SQLFilter(TO.CVB_Description));
            }
            if (!string.IsNullOrEmpty(TO.CVB_Remark))
            {
                filter += string.Format(" and CC_Name like '%{0}%' ", StringHelper.SQLFilter(TO.CVB_Remark));
            }
            if (TO.CVB_CreateTime > Convert.ToDateTime("1979-01-01"))
            {
                filter += string.Format(" and CVB_CreateTime >= '{0}' ", TO.CVB_CreateTime.ToString("yyyy-MM-dd"));
            }
            if (TO.CVB_OperationTime > Convert.ToDateTime("1979-01-01"))
            {
                filter += string.Format(" and CVB_CreateTime < '{0}' ", TO.CVB_OperationTime.AddDays(1).ToString("yyyy-MM-dd"));
            }
            #endregion

            string sort = " CVB_ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "Call_VisitBillList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }



    }
}