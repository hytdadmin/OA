using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_SatisfactionTO
	{
   		     
      	/// <summary>
		/// CS_ID
        /// </summary>		
		private int _cs_id;
        public int CS_ID
        {
            get{ return _cs_id; }
            set{ _cs_id = value; }
        }        
		/// <summary>
		/// CS_CWB_ID
        /// </summary>		
		private int _cs_cwb_id;
        public int CS_CWB_ID
        {
            get{ return _cs_cwb_id; }
            set{ _cs_cwb_id = value; }
        }        
		/// <summary>
		/// CS_UserCode
        /// </summary>		
		private string _cs_usercode;
        public string CS_UserCode
        {
            get{ return _cs_usercode; }
            set{ _cs_usercode = value; }
        }        
		/// <summary>
		/// CS_CreateDate
        /// </summary>		
		private DateTime _cs_createdate;
        public DateTime CS_CreateDate
        {
            get{ return _cs_createdate; }
            set{ _cs_createdate = value; }
        }        
		/// <summary>
		/// CS_CSI_ID
        /// </summary>		
		private int _cs_csi_id;
        public int CS_CSI_ID
        {
            get{ return _cs_csi_id; }
            set{ _cs_csi_id = value; }
        }        
		/// <summary>
		/// CS_Remark
        /// </summary>		
		private string _cs_remark;
        public string CS_Remark
        {
            get{ return _cs_remark; }
            set{ _cs_remark = value; }
        }        
		   
	}
}