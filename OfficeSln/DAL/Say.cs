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
using Models.TO;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:09
Description: DAL层 Say
*/
namespace DAL
{

	public class SayDAL
	{
		LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddSay(Say entity)
		{
			linqHelper.InsertEntity<Say>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateSay(Say entity)
		{
			linqHelper.UpdateEntity<Say>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Say GetSayEntity(int ID)
		{
            return linqHelper.GetEntity<Say>(c => c.Id == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteSay(int FID)
        {
            linqHelper.DeleteEntity<Say>(c => c.Id == FID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Say> GetSayList()
		{
			return linqHelper.GetList<Say>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetSayList(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Say] ";
            string pk = " ID ";
            string fields = " * ";
            string filter ="";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
			//	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            if (!string.IsNullOrEmpty(TO.Contents))
            {
                filter += string.Format(" Contents like '%{0}%' ", StringHelper.SQLFilter(TO.Contents));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "SayList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddSayReturn(Say entity)
        {
            try
            {
                linqHelper.InsertEntity<Say>(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 获取最新的一条说说
        /// </summary>
        public Say GetSayEntityByPublishId(string publishUserCode)
        {
            //return linqHelper.GetEntity<Say>(c => c.PublishId == publishId && c.IsDel==1);
            return linqHelper.GetList<Say>(c => c.PublishUserCode == publishUserCode && c.IsDel == 1).OrderByDescending(c => c.PublishTime).FirstOrDefault();
        }

        /// <summary>
        /// 获取最新N条说说消息
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<SayList> GetSayListByTop(int top)
        {
            var result = from s in linqHelper.GetList<Say>(l => l.IsDel == 1)
                         join u in linqHelper.GetList<UserInfo>() on s.PublishUserCode equals u.UserCode
                         select new SayList
                         {
                             Id = s.Id,
                             Contents = s.Contents,
                             PublishTime = s.PublishTime,
                             UserName = u.UserName,
                             UserId = u.UserID,
                             UserCode=u.UserCode
                         };
            if (result != null)
                return result.OrderByDescending(l => l.PublishTime).Take(top).ToList();
            return new List<SayList>();
        }

        public int GetSayListPagerCount(string contents) {
            var result = linqHelper.GetList<Say>();
            if (!string.IsNullOrEmpty(contents))
            {
                result = result.Where(l => l.Contents.Contains(contents)).ToList();
            }
            return result.Count();
        }

        public List<Say> GetSayListPager(int pageIndex, int pageSize, string contents)
        {
            var result = linqHelper.GetList<Say>();
            if (!string.IsNullOrEmpty(contents))
            {
                result = result.Where(l => l.Contents.Contains(contents)).ToList();
            }
            if (pageSize > 0)
            {
                result = result.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }
            return result.OrderByDescending(l => l.PublishTime).ToList();
        }


        /// <summary>
        /// 左侧说说列表
        /// 获取实体分页
        /// </summary>
        public DataTable GetSayListByLeft(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " Say s left join UserInfo u on s.PublishUserCode=u.UserCode ";
            string pk = " s.ID ";
            string fields = " s.Id,s.Contents,s.PublishTime,u.UserName,u.UserID,u.UserCode ";
            string filter = "";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件


            //if (!string.IsNullOrEmpty(TO.MC))
            //{
            //	filter += string.Format(" and MC like '%{0}%' ", StringHelper.SQLFilter(TO.MC));
            //}
            filter = " s.isdel=1 ";
            if (!string.IsNullOrEmpty(TO.Contents))
            {
                filter += string.Format(" Contents like '%{0}%' ", StringHelper.SQLFilter(TO.Contents));
            }

            #endregion

            string sort = " s.PublishTime DESC ";//排序
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "SayList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 多表连接查询
        /// </summary>
        public DataTable GetSayListByTB(SayTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " Say j left join UserInfo u on j.PublishUserCode=u.UserCode ";
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
            if (TO.DepId > 0)
            {
                filter += string.Format(" and u.DepartmentId={0} ", TO.DepId);
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
	}
}