using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYTD.CAPlatform.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 登入名
        /// </summary>
        public string LoginName
        {
            get;
            set;
        }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户角色  如普通用户、管理员、超级管理员
        /// </summary>
        public int? UserRole
        {
            get;
            set;
        }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 用户部门
        /// </summary>
        public int? UserDepartment
        {
            get;
            set;
        }
        /// <summary>
        /// 用户子部门
        /// </summary>
        public int? UserSubSector
        {
            get;
            set;
        }
        /// <summary>
        /// 获取当前页地址
        /// </summary>
        public string FromPage
        {
            get;
            set;
        }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP
        {
            get;
            set;
        }
        /// <summary>
        /// 电脑标识
        /// </summary>
        public string PCID
        {
            get;
            set;
        }
    }
}