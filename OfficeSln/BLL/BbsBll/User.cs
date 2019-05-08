using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Models;
using HYTD.BBS.Model.TO;
using HYTD.BBS.DAL;

/*
Author：liulei
Version：1.0
Date：2013-10-22 14:34:35
Description: BLL层 User
*/
namespace HYTD.BBS.BLL
{


	public class UserBLL
	{
   		 
   		 UserDAL service = new UserDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddUser(User entity)
        {
            service.AddUser(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUser(User entity)
        {
            new UserDAL().UpdateUser(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public User GetUserEntity(int FID)
        {
            return service.GetUserEntity(FID);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public User GetUserEntity(string strLogiName)
        {
            return service.GetUserEntity(strLogiName);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool ExsisUserEntity(string strLogiName)
        {
            return service.ExsisUserEntity(strLogiName);
        }

		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUser(int FID)
        {
            service.DeleteUser(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserList()
        {
            return service.GetUserList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserList(UserTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetUserList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}