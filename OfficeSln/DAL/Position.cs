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
Date：2013-09-17 10:59:07
Description: DAL层 Position
*/
namespace DAL
{

	public class PositionDAL
	{
		LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddPosition(Position entity)
		{
			linqHelper.InsertEntity<Position>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdatePosition(Position entity)
		{
			linqHelper.UpdateEntity<Position>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public Position GetPositionEntity(int ID)
		{
            return linqHelper.GetEntity<Position>(c => c.Id == ID);
		}
		
		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeletePosition(int FID)
        {
            linqHelper.DeleteEntity<Position>(c => c.Id == FID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<Position> GetPositionList()
		{
			return linqHelper.GetList<Position>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetPositionList(PositionTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [Position] ";
            string pk = " ID ";
            string fields = " * ";
            string filter =" 1=1 ";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);
            
            #region 组织查询条件


            if (!string.IsNullOrEmpty(TO.Name))
            {
                filter += string.Format(" and Name like '%{0}%' ", StringHelper.SQLFilter(TO.Name));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "PositionList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}