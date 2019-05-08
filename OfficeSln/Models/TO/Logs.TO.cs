using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class LogsTO
	{
   		     
      	/// <summary>
		/// 日志记录表
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 记录类型（登录）
        /// </summary>		
		private string _typename;
        public string TypeName
        {
            get{ return _typename; }
            set{ _typename = value; }
        }        
		/// <summary>
		/// 记录用户
        /// </summary>		
		private string _usercode;
        public string UserCode
        {
            get{ return _usercode; }
            set{ _usercode = value; }
        }        
		/// <summary>
		/// 日志内容
        /// </summary>		
		private string _contents;
        public string Contents
        {
            get{ return _contents; }
            set{ _contents = value; }
        }        
		/// <summary>
		/// 添加时间
        /// </summary>		
		private DateTime _addtime;
        public DateTime AddTime
        {
            get{ return _addtime; }
            set{ _addtime = value; }
        }        
		/// <summary>
		/// 是否删除
        /// </summary>		
		private int _isdel;
        public int IsDel
        {
            get{ return _isdel; }
            set{ _isdel = value; }
        }        
		   
	}
}