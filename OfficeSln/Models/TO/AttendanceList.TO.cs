using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class AttendanceListTO
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
		/// MaxTime
        /// </summary>		
		private DateTime _maxtime;
        public DateTime MaxTime
        {
            get{ return _maxtime; }
            set{ _maxtime = value; }
        }        
		/// <summary>
		/// MinTime
        /// </summary>		
		private DateTime _mintime;
        public DateTime MinTime
        {
            get{ return _mintime; }
            set{ _mintime = value; }
        }        
		/// <summary>
		/// WorkDay
        /// </summary>		
		private string _workday;
        public string WorkDay
        {
            get{ return _workday; }
            set{ _workday = value; }
        }        
		/// <summary>
		/// Remark
        /// </summary>		
		private string _remark;
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }        
		   
	}
}