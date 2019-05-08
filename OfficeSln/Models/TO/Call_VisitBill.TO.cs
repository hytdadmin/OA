using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_VisitBillTO
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _cvb_id;
        public int CVB_ID
        {
            get{ return _cvb_id; }
            set{ _cvb_id = value; }
        }        
		/// <summary>
		/// 客户id
        /// </summary>		
		private int _cvb_ccid;
        public int CVB_CCID
        {
            get{ return _cvb_ccid; }
            set{ _cvb_ccid = value; }
        }        
		/// <summary>
		/// 工单id
        /// </summary>		
		private int _cvb_cwb_id;
        public int CVB_CWB_ID
        {
            get{ return _cvb_cwb_id; }
            set{ _cvb_cwb_id = value; }
        }        
		/// <summary>
		/// 来电人姓名
        /// </summary>		
		private string _cvb_callinusername;
        public string CVB_CallInUserName
        {
            get{ return _cvb_callinusername; }
            set{ _cvb_callinusername = value; }
        }        
		/// <summary>
		/// 来电人电话
        /// </summary>		
		private string _cvb_callintel;
        public string CVB_CallInTel
        {
            get{ return _cvb_callintel; }
            set{ _cvb_callintel = value; }
        }        
		/// <summary>
		/// 来电人邮箱
        /// </summary>		
		private string _cvb_callinemail;
        public string CVB_CallInEmail
        {
            get{ return _cvb_callinemail; }
            set{ _cvb_callinemail = value; }
        }        
		/// <summary>
		/// 工单类型（来电、上门）
        /// </summary>		
		private int _cvb_type;
        public int CVB_Type
        {
            get{ return _cvb_type; }
            set{ _cvb_type = value; }
        }        
		/// <summary>
		/// 咨询软件（Windows、Office、其它）
        /// </summary>		
		private int _cvb_softtype;
        public int CVB_SoftType
        {
            get{ return _cvb_softtype; }
            set{ _cvb_softtype = value; }
        }        
		/// <summary>
		/// 咨询类型（咨询、报错、操作、其它）
        /// </summary>		
		private int _cvb_calltype;
        public int CVB_CallType
        {
            get{ return _cvb_calltype; }
            set{ _cvb_calltype = value; }
        }        
		/// <summary>
		/// 服务类型（Ca平台、kms、wsus、其它）
        /// </summary>		
		private int _cvb_servicetype;
        public int CVB_ServiceType
        {
            get{ return _cvb_servicetype; }
            set{ _cvb_servicetype = value; }
        }        
		/// <summary>
		/// 问题描述
        /// </summary>		
		private string _cvb_description;
        public string CVB_Description
        {
            get{ return _cvb_description; }
            set{ _cvb_description = value; }
        }        
		/// <summary>
		/// 解决办法
        /// </summary>		
		private string _cvb_solution;
        public string CVB_Solution
        {
            get{ return _cvb_solution; }
            set{ _cvb_solution = value; }
        }        
		/// <summary>
		/// 创建人
        /// </summary>		
		private int _cvb_creater;
        public int CVB_Creater
        {
            get{ return _cvb_creater; }
            set{ _cvb_creater = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _cvb_createtime;
        public DateTime CVB_CreateTime
        {
            get{ return _cvb_createtime; }
            set{ _cvb_createtime = value; }
        }        
		/// <summary>
		/// 指定服务人
        /// </summary>		
		private int _cvb_foruser;
        public int CVB_ForUser
        {
            get{ return _cvb_foruser; }
            set{ _cvb_foruser = value; }
        }        
		/// <summary>
		/// 操作时间
        /// </summary>		
		private DateTime _cvb_operationtime;
        public DateTime CVB_OperationTime
        {
            get{ return _cvb_operationtime; }
            set{ _cvb_operationtime = value; }
        }        
		/// <summary>
		/// 工单状态（服务中、已完成、已回访）
        /// </summary>		
		private int _cvb_status;
        public int CVB_Status
        {
            get{ return _cvb_status; }
            set{ _cvb_status = value; }
        }        
		/// <summary>
		/// 备注
        /// </summary>		
		private string _cvb_remark;
        public string CVB_Remark
        {
            get{ return _cvb_remark; }
            set{ _cvb_remark = value; }
        }        
		/// <summary>
		/// CVB_OperationUser
        /// </summary>		
		private int _cvb_operationuser;
        public int CVB_OperationUser
        {
            get{ return _cvb_operationuser; }
            set{ _cvb_operationuser = value; }
        }        
		   
	}
}