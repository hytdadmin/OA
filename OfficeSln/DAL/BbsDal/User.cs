using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using Models;
using HYTD.BBS.Model.TO;
/*
Author：liulei
Version：1.0
Date：2013-10-22 14:34:36
Description: DAL层 User
*/
namespace HYTD.BBS.DAL
{

	public class UserDAL
	{
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void AddUser(User entity)
		{
			linqHelper.InsertEntity<User>(entity);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void UpdateUser(User entity)
		{
			linqHelper.UpdateEntity<User>(entity);
		}
		
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public User GetUserEntity(int ID)
		{
			return linqHelper.GetEntity<User>(c => c.ID == ID);
		}
		/// <summary>
		/// 获取一个实体
		/// </summary>
		public User GetUserEntity(string LoginName)
		{
            return linqHelper.GetEntity<User>(c => c.LoginName == LoginName);
		}	
		/// <summary>
		/// 获取一个实体
		/// </summary>
        public bool ExsisUserEntity(string LoginName)
		{
            return linqHelper.GetList<User>(c => c.LoginName == LoginName).ToArray().Length>0;
		}		

		
		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int ID)
        {
            linqHelper.DeleteEntity<User>(c => c.ID == ID);
        }
		
		
		
		/// <summary>
		/// 获取全部实体集合
		/// </summary>
		public List<User> GetUserList()
		{
			return linqHelper.GetList<User>();
		}
		

		/// <summary>
		/// 获取实体分页
		/// </summary>
        public DataTable GetUserList(UserTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [User] ";
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "UserList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }


   
	}
}