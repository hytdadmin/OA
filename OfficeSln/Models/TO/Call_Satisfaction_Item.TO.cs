using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_Satisfaction_ItemTO
	{
   		     
      	/// <summary>
		/// CSI_ID
        /// </summary>		
		private int _csi_id;
        public int CSI_ID
        {
            get{ return _csi_id; }
            set{ _csi_id = value; }
        }        
		/// <summary>
		/// 调查类型
        /// </summary>		
		private int _csi_type;
        public int CSI_type
        {
            get{ return _csi_type; }
            set{ _csi_type = value; }
        }        
		/// <summary>
		/// 调查描述
        /// </summary>		
		private string _csi_name;
        public string CSI_Name
        {
            get{ return _csi_name; }
            set{ _csi_name = value; }
        }        
		/// <summary>
		/// 1 非常满意 2满意 3一般 4不满意
        /// </summary>		
		private int _csi_result;
        public int CSI_Result
        {
            get{ return _csi_result; }
            set{ _csi_result = value; }
        }        
		   
	}
}