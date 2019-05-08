using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class UserInfoTO
	{
   		     
      	/// <summary>
		/// UserID
        /// </summary>		
		private int _userid;
        public int UserID
        {
            get{ return _userid; }
            set{ _userid = value; }
        }        
		/// <summary>
		/// UserCode
        /// </summary>		
		private string _usercode;
        public string UserCode
        {
            get{ return _usercode; }
            set{ _usercode = value; }
        }        
		/// <summary>
		/// UserName
        /// </summary>		
		private string _username;
        public string UserName
        {
            get{ return _username; }
            set{ _username = value; }
        }        
		/// <summary>
		/// 0离职，1在职
        /// </summary>		
		private int _userstatus;
        public int UserStatus
        {
            get{ return _userstatus; }
            set{ _userstatus = value; }
        }        
		/// <summary>
		/// Upwd
        /// </summary>		
		private string _upwd;
        public string Upwd
        {
            get{ return _upwd; }
            set{ _upwd = value; }
        }        
		/// <summary>
		/// HeadImg
        /// </summary>		
		private string _headimg=string.Empty;
        public string HeadImg
        {
            get{ return _headimg; }
            set{ _headimg = value; }
        }        
		/// <summary>
		/// 部门id对应Roles中id
        /// </summary>		
		private int? _departmentid;
        public int? DepartmentId
        {
            get{ return _departmentid; }
            set{ _departmentid = value; }
        }        
		/// <summary>
		/// 职位id对应Roles中id
        /// </summary>		
		private int? _posiid;
        public int? PosiId
        {
            get{ return _posiid; }
            set{ _posiid = value; }
        }        
		/// <summary>
		/// 是否是管理员，1为普通用户，0为管理员
        /// </summary>		
		private int? _isadmin;
        public int? IsAdmin
        {
            get{ return _isadmin; }
            set{ _isadmin = value; }
        }
        private string depName;
        /// <summary>
        /// 所在部门名称
        /// </summary>
        public string DepName
        {
            get { return depName; }
            set { depName = value; }
        }
        private string posiName;
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosiName
        {
            get { return posiName; }
            set { posiName = value; }
        }
		   
	}
}