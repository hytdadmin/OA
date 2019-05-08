using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using DBUtility;
using System.Data;
using HYTD.Common;
using Model.TO;
using System.Data.SqlClient;
using System.Configuration;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:05
Description: DAL层 Journal
*/
namespace DAL
{

	public class JournalDAL
	{
		LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddJournal(Journal entity)
		{
			linqHelper.InsertEntity<Journal>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateJournal(Journal entity)
		{
			linqHelper.UpdateEntity<Journal>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Journal GetJournalEntity(int ID)
		{
            return linqHelper.GetEntity<Journal>(c => c.Id == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteJournal(int FID)
        {
            linqHelper.DeleteEntity<Journal>(c => c.Id == FID);
        }

		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Journal> GetJournalList()
		{
			return linqHelper.GetList<Journal>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetJournalList(JournalTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Journal] ";
            string pk = " ID ";
            string fields = " * ";
            string filter ="";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
			//	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            filter += " isdel=1 ";
            if (!string.IsNullOrEmpty(TO.Contents))
            {
                filter += string.Format(" and Contents like '%{0}%' ", StringHelper.SQLFilter(TO.Contents));
            }

            if (!string.IsNullOrEmpty(TO.PublishUserCode))
            {
                filter = filter.Length != 0 ? filter + " and" : "";
                filter += string.Format(" PublishUserCode = '{0}' ", StringHelper.SQLFilter(TO.PublishUserCode));
            }
            if (!string.IsNullOrEmpty(TO.SearchTime))
            {
                filter += string.Format(" and datediff(day,publishTime,'{0}')=0", StringHelper.SQLFilter(TO.SearchTime));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "JournalList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


        /// <summary>
        /// 多表连接查询
        /// </summary>
        public DataTable GetJournalListByTB(JournalTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " Journal j left join UserInfo u on j.PublishUserCode=u.UserCode ";
            string pk = " j.ID ";
            string fields = " j.Id,j.Contents,j.PublishTime,j.PublishUserCode,u.UserName,u.HeadImg ";
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            filter += " j.IsDel=1 and u.UserStatus=1 ";
            if (!string.IsNullOrEmpty(TO.Contents))
            {
                filter += string.Format(" and j.Contents like '%{0}%' ", StringHelper.SQLFilter(TO.Contents));
            }
            if (TO.DepId > 0) {
                filter += string.Format(" and u.DepartmentId={0} ",TO.DepId);
            }
            if (!string.IsNullOrEmpty(TO.SearchTime))
            {
                filter += string.Format(" and datediff(day,publishTime,'{0}')=0", StringHelper.SQLFilter(TO.SearchTime));
            }
            #endregion

            string sort = " j.ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "JournalList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddJournalReturn(Journal entity)
        {
            try
            {
                //linqHelper.InsertEntity<Journal>(entity);
                AttendanceDataContext db = new AttendanceDataContext();
                db.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["KaoqinConnectionString"].ToString();
                db.Journal.InsertOnSubmit(entity);
                db.SubmitChanges();
                return entity.Id;
                //return true;
            }
            catch
            {
                //return false;
                return 0;
            }
        }


        /// <summary>
        /// 删除一条数据(只能删除自己的)
        /// </summary>
        /// <param name="id"></param>
        public bool DeleteJournalByUserCode(int FID,string userCode)
        {
            try
            {
                linqHelper.DeleteEntity<Journal>(c => c.Id == FID && c.PublishUserCode == userCode);
                return true;
            }
            catch
            {
                return false;
            }
        }
   
	}
}