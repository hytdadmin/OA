using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_WorkBillHistoryTO
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _cwbh_id;
        public int CWBH_ID
        {
            get{ return _cwbh_id; }
            set{ _cwbh_id = value; }
        }        
		/// <summary>
		/// 工单ID
        /// </summary>		
		private int _cwbh_cwb_id;
        public int CWBH_CWB_ID
        {
            get{ return _cwbh_cwb_id; }
            set{ _cwbh_cwb_id = value; }
        }        
		/// <summary>
		/// 问题描述
        /// </summary>		
		private string _cwbh_description;
        public string CWBH_Description
        {
            get{ return _cwbh_description; }
            set{ _cwbh_description = value; }
        }        
		/// <summary>
		/// 解决办法
        /// </summary>		
		private string _cwbh_solution;
        public string CWBH_Solution
        {
            get{ return _cwbh_solution; }
            set{ _cwbh_solution = value; }
        }        
		/// <summary>
		/// 服务人
        /// </summary>		
		private int _cwbh_userid;
        public int CWBH_UserID
        {
            get{ return _cwbh_userid; }
            set{ _cwbh_userid = value; }
        }        
		/// <summary>
		/// 服务时间
        /// </summary>		
		private DateTime _cwbh_operationtime;
        public DateTime CWBH_OperationTime
        {
            get{ return _cwbh_operationtime; }
            set{ _cwbh_operationtime = value; }
        }        
		/// <summary>
		/// 工单状态
        /// </summary>		
		private int _cwbh_status;
        public int CWBH_Status
        {
            get{ return _cwbh_status; }
            set{ _cwbh_status = value; }
        }        
		   
	}
}