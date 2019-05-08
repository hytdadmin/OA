using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class DepartmentTO
	{
   		     
      	/// <summary>
		/// 部门角色(部门、职称)ID
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 上级部门id
        /// </summary>		
		private int _parentid;
        public int ParentId
        {
            get{ return _parentid; }
            set{ _parentid = value; }
        }        
		/// <summary>
		/// 部门名称
        /// </summary>		
		private string _rolename;
        public string RoleName
        {
            get{ return _rolename; }
            set{ _rolename = value; }
        }        
		/// <summary>
		/// 是否删除(默认1未删除)；1未删除、0删除
        /// </summary>		
		private int _isdel;
        public int IsDel
        {
            get{ return _isdel; }
            set{ _isdel = value; }
        }        
		   
	}
}