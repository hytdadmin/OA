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
Date：2013-10-12 15:25:03
Description: DAL层 VisitTb
*/
namespace DAL
{

	public class VisitTbDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddVisitTb(VisitTb entity)
		{
			linqHelper.InsertEntity<VisitTb>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateVisitTb(VisitTb entity)
		{
			linqHelper.UpdateEntity<VisitTb>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public VisitTb GetVisitTbEntity(int ID)
		{
            return linqHelper.GetEntity<VisitTb>(c => c.Id == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVisitTb(int FID)
        {
            linqHelper.DeleteEntity<VisitTb>(c => c.Id == FID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<VisitTb> GetVisitTbList()
		{
			return linqHelper.GetList<VisitTb>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetVisitTbList(VisitTbTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [VisitTb] ";
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "VisitTbList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public int GetVisitCount(int min)
        {
            DateTime dn = DateTime.Now;
            var result= linqHelper.GetList<VisitTb>().Where(l => l.IsDel == 1 && l.VisitTime <= dn && l.VisitTime >= dn.AddMinutes(-min));
            return (from r in result select r.UserCode).Distinct().Count();
        }
   
	}
}