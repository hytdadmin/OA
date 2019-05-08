using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class AttendanceDetailTO
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
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
		/// Time
        /// </summary>		
		private DateTime _time;
        public DateTime Time
        {
            get{ return _time; }
            set{ _time = value; }
        }        
		/// <summary>
		/// 年月
        /// </summary>		
		private string _month;
        public string Month
        {
            get{ return _month; }
            set{ _month = value; }
        }        
		   
	}
}