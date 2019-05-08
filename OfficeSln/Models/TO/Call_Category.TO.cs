using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_CategoryTO
	{
   		     
      	/// <summary>
		/// C_ID
        /// </summary>		
		private int _c_id;
        public int C_ID
        {
            get{ return _c_id; }
            set{ _c_id = value; }
        }        
		/// <summary>
		/// C_Name
        /// </summary>		
		private string _c_name;
        public string C_Name
        {
            get{ return _c_name; }
            set{ _c_name = value; }
        }        
		/// <summary>
		/// C_FID
        /// </summary>		
		private int _c_fid;
        public int C_FID
        {
            get{ return _c_fid; }
            set{ _c_fid = value; }
        }        
		/// <summary>
		/// C_Status
        /// </summary>		
		private int _c_status;
        public int C_Status
        {
            get{ return _c_status; }
            set{ _c_status = value; }
        }        
		   
	}
}