using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class VisitTbTO
	{
   		     
      	/// <summary>
		/// 用户访问时间表(在线统计)
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 用户code
        /// </summary>		
		private string _usercode;
        public string UserCode
        {
            get{ return _usercode; }
            set{ _usercode = value; }
        }        
		/// <summary>
		/// 访问时间
        /// </summary>		
		private DateTime _visittime;
        public DateTime VisitTime
        {
            get{ return _visittime; }
            set{ _visittime = value; }
        }        
		/// <summary>
		/// '访问的页面'
        /// </summary>		
		private string _visitpage;
        public string VisitPage
        {
            get{ return _visitpage; }
            set{ _visitpage = value; }
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