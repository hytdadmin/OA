using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBUtility;
using Models;
using System.Data;
using Models.TO;
using System.Data.SqlClient;
using HYTD.Common;

namespace DAL
{
    public class HolidaysDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddHolidaysTable(HolidaysTable entity)
        {
            linqHelper.InsertEntity<HolidaysTable>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateHolidaysTable(HolidaysTable entity)
        {
            linqHelper.UpdateEntity<HolidaysTable>(entity);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteHolidaysTable(int ID)
        {
            
            try
            {
                linqHelper.DeleteEntity<HolidaysTable>(c => c.ID == ID);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取一个实体
        /// </summary>
        public HolidaysTable GetHolidaysTableEntity(int ID)
        {
            return linqHelper.GetEntity<HolidaysTable>(c => c.ID == ID);
        }

        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<HolidaysTable> GetHolidaysTableList()
        {
            return linqHelper.GetList<HolidaysTable>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetHolidaysTableList(HolidaysTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " HolidaysTable left join UserInfo on HolidaysTable.UserCode=UserInfo.UserCode ";
            string pk = " ID ";
            string fields = " ID, HolidaysTable.UserCode,UserInfo.UserName, startTime,endTime, DayCount, HolidaysType, Remark,UserInfo.AnnualLeave ";
            string filter = " UserInfo.UserStatus=1 ";

            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.UserName))
            {
                filter += string.Format(" and UserName like '%{0}%' ", StringHelper.SQLFilter(TO.UserName));
            }
            if (TO.HolidaysType!=0)
            {
                filter += string.Format(" and HolidaysType={0} ", TO.HolidaysType);
            }
            if (!string.IsNullOrEmpty(TO.StratTime))
            {
                filter += string.Format(" and '{0}'<startTime ", TO.StratTime+" 00:00:00 ");
            }
            if (!string.IsNullOrEmpty(TO.EndTime))
            {
                filter += string.Format(" and '{0}'>endTime ", TO.EndTime + " 23:59:59 ");
            }
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "HolidaysTableList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取用户假期
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserHolidays(string userCode)
        {
            string sql = " select a.*,UserInfo.UserName,UserInfo.AnnualLeave from (select SUM(DayCount) as DayCount,HolidaysType, UserCode from HolidaysTable  group by HolidaysType,UserCode ) a  left join UserInfo on  a.UserCode=UserInfo.UserCode where UserInfo.UserCode=@userCode and UserInfo.UserStatus=1  order by HolidaysType ";
            SqlParameter sp = new SqlParameter("@userCode",SqlDbType.VarChar);
            sp.Value = userCode;
            DataSet ds= SqlHelper.Query(sql,sp);
            return ds.Tables[0];
        }


        /// <summary>
        /// 假期统计信息
        /// </summary>
        public DataTable GetHolidaysStatisInfor(HolidaysTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = "  UserInfo left join (select UserCode,sum(case HolidaysType when 1 then DayCount else 0 end) nianjia,sum(case HolidaysType when 2 then DayCount else 0 end) daoxiu,sum(case HolidaysType when 3 then DayCount else 0 end) jiaban from HolidaysTable group by UserCode)a on a.UserCode=UserInfo.UserCode   ";
            string pk = " ID ";
            string fields = " UserInfo.UserCode,UserInfo.UserName, isnull(a.nianjia,0) nianjia,isnull(a.daoxiu,0) daoxiu ,isnull(a.jiaban,0) jiaban,UserInfo.AnnualLeave  ";
            string filter = "  UserInfo.UserStatus=1  ";

            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.UserCode) && TO.UserCode!="0")
            {
                filter += string.Format(" and UserInfo.UserCode='{0}' ", StringHelper.SQLFilter(TO.UserCode));
            }
           
            #endregion

            string sort = " UserInfo.UserCode ASC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "HolidaysTableList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
    }
}
