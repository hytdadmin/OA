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
/*
Author：liulei
Version：1.0
Date：2013-09-22 15:55:19
Description: DAL层 DownloadCenter
*/
namespace DAL
{

	public class DownloadCenterDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddDownloadCenter(DownloadCenter entity)
		{
			linqHelper.InsertEntity<DownloadCenter>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateDownloadCenter(DownloadCenter entity)
		{
			linqHelper.UpdateEntity<DownloadCenter>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public DownloadCenter GetDownloadCenterEntity(int ID)
		{
            return linqHelper.GetEntity<DownloadCenter>(c => c.Id == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDownloadCenter(int FID)
        {
            linqHelper.DeleteEntity<DownloadCenter>(c => c.Id == FID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<DownloadCenter> GetDownloadCenterList()
		{
			return linqHelper.GetList<DownloadCenter>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetDownloadCenterList(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [DownloadCenter] ";
            string pk = " ID ";
            string fields = " * ";
            string filter ="";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
			//	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}

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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "DownloadCenterList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取实体分页(可设定返回值)
        /// </summary>
        public DataTable GetDownloadCenterListByFileds(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy,string fileds, out int rowCount)
        {
            string table = " [DownloadCenter] ";
            string pk = " ID ";
            string fields = fileds;
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            filter += " isdel=1 ";
            if (!string.IsNullOrEmpty(TO.Title))
            {
                filter += string.Format(" and Title like '%{0}%' ", StringHelper.SQLFilter(TO.Title));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "DownloadCenterList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


        /// <summary>
        /// 获取实体分页
        /// 后台使用
        /// </summary>
        public DataTable GetDownloadCenterListByBack(DownloadCenterTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " DownloadCenter b left join UserInfo u on b.PublishUserCode=u.UserCode left join Department d on u.DepartmentId=d.Id ";
            string pk = " b.ID ";
            string fields = " b.Id,b.PublishUserCode,b.Title,b.AffixOldName,b.PublishTime,u.UserName as PubName,d.RoleName as PubDep ";
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            filter += " b.isdel=1 ";
            if (!string.IsNullOrEmpty(TO.Title))
            {
                filter += string.Format(" and b.Title like '%{0}%' ", StringHelper.SQLFilter(TO.Title));
            }
            if (!string.IsNullOrEmpty(TO.PubName))
                filter += string.Format(" and u.PubName like '%{0}%' ", StringHelper.SQLFilter(TO.PubName));
            if (TO.PubDepId > 0)
                filter += string.Format(" and d.id={0} ", TO.PubDepId);

            //根据开始日期进行选择
            if (TO.StartDt.HasValue)
            {
                DateTime beginDt = DateTime.Parse(TO.StartDt.Value.ToShortDateString());
                filter += string.Format(" and b.PublishTime>='{0}' ", beginDt);
            }
            //根据结束日期进行选择
            if (TO.EndDt.HasValue)
            {
                var endDate = DateTime.Parse(TO.EndDt.Value.ToShortDateString() + " 23:59:59");
                filter += string.Format(" and b.PublishTime<='{0}' ", endDate);
            }
            #endregion

            string sort = " b.ID DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "BulletinList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">所有id，格式,1,2,3,4,</param>
        /// <returns></returns>
        public bool DelDownloadCenters(string ids)
        {
            ids = ids.Substring(0, 1) == "," ? ids : "," + ids;
            ids = ids.Substring(ids.Length - 1, 1) == "," ? ids : ids + ",";
            var del = linqHelper.GetList<DownloadCenter>().Where(l => ids.Contains("," + l.Id + ","));
            try
            {
                linqHelper.DeleteEntities<DownloadCenter>(del.ToList());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddDownloadCenterReturn(DownloadCenter entity)
        {
            try
            {
                linqHelper.InsertEntity<DownloadCenter>(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
	}
}