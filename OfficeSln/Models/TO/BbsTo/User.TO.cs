using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.BBS.Model.TO
{

	public class UserTO
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 登录名
        /// </summary>		
		private string _loginname;
        public string LoginName
        {
            get{ return _loginname; }
            set{ _loginname = value; }
        }        
		/// <summary>
		/// 姓名
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }        
		/// <summary>
		/// 邮箱
        /// </summary>		
		private string _email;
        public string Email
        {
            get{ return _email; }
            set{ _email = value; }
        }        
		/// <summary>
		/// 电话
        /// </summary>		
		private string _phone;
        public string Phone
        {
            get{ return _phone; }
            set{ _phone = value; }
        }        
		/// <summary>
		/// 部门guid
        /// </summary>		
		private int _deptid;
        public int DeptID
        {
            get{ return _deptid; }
            set{ _deptid = value; }
        }        
		/// <summary>
		/// 时间
        /// </summary>		
		private DateTime _upadatetime;
        public DateTime UpadateTime
        {
            get{ return _upadatetime; }
            set{ _upadatetime = value; }
        }        
		/// <summary>
		/// AD域Guid
        /// </summary>		
		private string _guid;
        public string Guid
        {
            get{ return _guid; }
            set{ _guid = value; }
        }        
		   
	}
}