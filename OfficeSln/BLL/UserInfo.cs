using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Model.TO;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:59:10
Description: BLL层 UserInfo
*/
namespace BLL
{


	public class UserInfoBLL
	{
   		 
   		 UserInfoDAL service = new UserInfoDAL();




         #region  Method


         /// <summary>
         /// 增加一条数据
         /// </summary>
         /// <param name="entity"></param>
         public void AddUserInfo(UserInfo entity)
         {
             service.AddUserInfo(entity);
         }


         /// <summary>
         /// 更新一条数据
         /// </summary>
         /// <param name="entity"></param>
         public void UpdateUserInfo(UserInfo entity)
         {
             new UserInfoDAL().UpdateUserInfo(entity);
         }


         /// <summary>
         /// 获取实体
         /// </summary>
         /// <param name="guid"></param>
         /// <returns></returns>
         public UserInfo GetUserInfoEntity(int FID)
         {
             return service.GetUserInfoEntity(FID);
         }



         /// <summary>
         /// 删除一条数据
         /// </summary>
         /// <param name="id"></param>
         public void DeleteUserInfo(int FID)
         {
             service.DeleteUserInfo(FID);
         }


         /// <summary>
         /// 获得数据列表
         /// </summary>
         /// <returns></returns>
         public List<UserInfo> GetUserInfoList()
         {
             return service.GetUserInfoList();
         }


         /// <summary>
         /// 获得数据列表分页
         /// </summary>
         /// <returns></returns>
         public DataTable GetUserInfoList(UserInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
         {
             return service.GetUserInfoList(TO, pageIndex, pageSize, orderBy, out rowCount);
         }

         /// <summary>
         /// 自定义获得数据列表分页
         /// </summary>
         /// <returns></returns>
         public DataTable GetUserInfoList(UserInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount,string str)
         {
             return service.GetUserInfoList(TO, pageIndex, pageSize, orderBy, out rowCount,"");
         }

         #endregion
   
        /// <summary>
        /// 获取用户的信息
        /// </summary>
        public DataTable GetUserInfoByUserCode(string userCode)
        {
            return service.GetUserInfoByUserCode(userCode);
        }
        /// <summary>
        /// 根据用户名和密码
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserMode(string userCode, string pwd)
        {
            return service.GetUserMode(userCode, pwd);
        }
        
        /// <summary>
        /// 根据userid获取usercode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserCodeById(int id)
        {
            return service.GetUserCodeById(id);
        }
        /// <summary>
        /// 根据usercode获取用户信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserInfo GetUserByUserCode(string userCode)
        {
            return service.GetUserByUserCode(userCode);
        }
        /// <summary>
        /// 获取用户的信息
        /// </summary>
        public UserInfoTO GetUserInfoByCode(string userCode)
        {
            return service.GetUserInfoByCode(userCode);
        }
        /// <summary>
        /// 获取用户的信息
        /// </summary>
        public string GetUserInfoEntityByUserCode(int ID)
        {
            string strUserName = string.Empty;
            UserInfo user = service.GetUserInfoEntityByUserCode(ID);
            if (user != null)
                strUserName = user.UserName;

            return strUserName;
        }
	}
}