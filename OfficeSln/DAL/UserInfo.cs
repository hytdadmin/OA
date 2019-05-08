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
Date：2013-09-17 10:59:10
Description: DAL层 UserInfo
*/
namespace DAL
{

    public class UserInfoDAL
    {
        LinqHelper<AttendanceDataContext> linqHelper = new LinqHelper<AttendanceDataContext>();



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddUserInfo(UserInfo entity)
        {
            linqHelper.InsertEntity<UserInfo>(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateUserInfo(UserInfo entity)
        {
            linqHelper.UpdateEntity<UserInfo>(entity);
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public UserInfo GetUserInfoEntity(int ID)
        {
            return linqHelper.GetEntity<UserInfo>(c => c.UserID == ID);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUserInfo(int FID)
        {
            linqHelper.DeleteEntity<UserInfo>(c => c.UserID == FID);
        }



        /// <summary>
        /// 获取全部实体集合
        /// </summary>
        public List<UserInfo> GetUserInfoList()
        {
            return linqHelper.GetList<UserInfo>();
        }


        /// <summary>
        /// 获取实体分页
        /// </summary>
        public DataTable GetUserInfoList(UserInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            string table = " [UserInfo] ";
            string pk = " ID ";
            string fields = " * ";
            string filter = " UserStatus=1 ";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件

            if (!string.IsNullOrEmpty(TO.UserName))
            {
                filter += string.Format(" and UserName like '%{0}%' ", StringHelper.SQLFilter(TO.UserName));
            }
            if (!string.IsNullOrEmpty(TO.UserCode))
            {
                filter += string.Format(" and UserCode like '%{0}%' ", StringHelper.SQLFilter(TO.UserCode));
            }
            if (TO.DepartmentId!=null&&TO.DepartmentId != 0)
            {
                filter += string.Format(" and DepartmentId={0} ", StringHelper.SQLFilter(TO.DepartmentId.ToString()));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "UserInfoList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }

        /// <summary>
        /// 自定义获取实体分页
        /// </summary>
        public DataTable GetUserInfoList(UserInfoTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount,string str)
        {
            string table = " UserInfo left join Department on UserInfo.DepartmentId=Department.Id left join Position on UserInfo.PosiId=Position.Id ";
            string pk = " UserID ";
            string fields = " UserID, UserCode,EntyTime, UserName, UserStatus, Upwd, HeadImg, DepartmentId, PosiId, IsAdmin, Tel, Email,RoleName,Name ";
            string filter = " 1=1 ";// string.Format(" Status={0} ", ConstantsManager.JiLuZhuangTai.Normal);

            #region 组织查询条件

            if (!string.IsNullOrEmpty(TO.UserName))
            {
                filter += string.Format(" and UserName like '%{0}%' ", StringHelper.SQLFilter(TO.UserName));
            }
            if (!string.IsNullOrEmpty(TO.UserCode))
            {
                filter += string.Format(" and UserCode like '%{0}%' ", StringHelper.SQLFilter(TO.UserCode));
            }
            if (TO.DepartmentId != null && TO.DepartmentId != 0)
            {
                filter += string.Format(" and DepartmentId={0} ", StringHelper.SQLFilter(TO.DepartmentId.ToString()));
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

            DataSet ds = SqlHelper.RunProcedure("SP_DividePage", parameters, "UserInfoList");
            rowCount = (int)parameters[7].Value;
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取用户的信息
        /// </summary>
        public DataTable GetUserInfoByUserCode(string userCode)
        {
            string strsql = string.Format("select u.userId,u.username,u.userCode,u.headimg,u.departmentid,u.posiid,d.rolename,p.name from UserInfo u,Department d ,position p where u.DepartmentId=d.Id and u.PosiId=p.id and u.UserCode='{0}'", userCode);
            DataTable dt = SqlHelper.Query(strsql.ToString()).Tables[0];
            return dt;
        }
        /// <summary>
        /// 根据用户名和密码
        /// </summary>
        /// <returns></returns>
        public UserInfo GetUserMode(string userCode, string pwd)
        {
            return linqHelper.GetEntity<UserInfo>(c => c.UserCode == userCode && c.Upwd == pwd);
        }

        /// <summary>
        /// 根据userid获取usercode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserCodeById(int id) {
            return linqHelper.GetEntity<UserInfo>(l => l.UserID == id).UserCode;
        }

        /// <summary>
        /// 根据usercode获取用户信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserInfo GetUserByUserCode(string userCode)
        {
            return linqHelper.GetEntity<UserInfo>(l => l.UserCode == userCode);
        }

        /// <summary>
        /// 获取用户的信息
        /// </summary>
        public UserInfoTO GetUserInfoByCode(string userCode)
        {
            //string strsql = string.Format("select u.userId,u.username,u.userCode,u.headimg,u.departmentid,u.posiid,d.rolename,p.name from UserInfo u,Department d ,position p where u.DepartmentId=d.Id and u.PosiId=p.id and u.UserCode='{0}'", userCode);
            //DataTable dt = SqlHelper.Query(strsql.ToString()).Tables[0];
            //return dt;
            UserInfoTO to = new UserInfoTO();
            var user=linqHelper.GetEntity<UserInfo>(l => l.UserCode == userCode);
            if (user != null&&user.UserID>0)
            {
                to.DepartmentId = user.DepartmentId;
                to.HeadImg = user.HeadImg;
                to.IsAdmin = user.IsAdmin;
                to.PosiId = user.PosiId;
                to.UserCode = user.UserCode;
                to.UserID = user.UserID;
                to.UserName = user.UserName;
                //to.UserStatus = user.UserStatus;
                var dep= linqHelper.GetEntity<Department>(l => l.Id == user.DepartmentId);
                to.DepName = (dep != null && dep.Id > 0) ? dep.RoleName : "";
                var posi=linqHelper.GetEntity<Position>(l => l.Id == user.PosiId);
                to.PosiName = (posi != null && posi.Id > 0) ? posi.Name : "";
            }
            return to;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        public UserInfo GetUserInfoEntityByUserCode(int UserCode)
        {
            return linqHelper.GetEntity<UserInfo>(c => c.UserCode == UserCode.ToString());
        }

    }
}