using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_WorkBillTO
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _cwb_id;
        public int CWB_ID
        {
            get{ return _cwb_id; }
            set{ _cwb_id = value; }
        }        
		/// <summary>
		/// 客户id
        /// </summary>		
		private int _cwb_ccid;
        public int CWB_CCID
        {
            get{ return _cwb_ccid; }
            set{ _cwb_ccid = value; }
        }
        /// <summary>
        /// 工单编号
        /// </summary>		
        private string _cwb_code;
        public string CWB_Code
        {
            get { return _cwb_code; }
            set { _cwb_code = value; }
        } 
		/// <summary>
		/// 来电人姓名
        /// </summary>		
		private string _cwb_callinusername;
        public string CWB_CallInUserName
        {
            get{ return _cwb_callinusername; }
            set{ _cwb_callinusername = value; }
        }        
		/// <summary>
		/// 来电人电话
        /// </summary>		
		private string _cwb_callintel;
        public string CWB_CallInTel
        {
            get{ return _cwb_callintel; }
            set{ _cwb_callintel = value; }
        }        
		/// <summary>
		/// 来电人邮箱
        /// </summary>		
		private string _cwb_callinemail;
        public string CWB_CallInEmail
        {
            get{ return _cwb_callinemail; }
            set{ _cwb_callinemail = value; }
        }        
		/// <summary>
		/// 工单类型（来电、上门）
        /// </summary>		
		private int _cwb_type;
        public int CWB_Type
        {
            get{ return _cwb_type; }
            set{ _cwb_type = value; }
        }        
		/// <summary>
		/// 咨询软件（Windows、Office、其它）
        /// </summary>		
		private int _cwb_softtype;
        public int CWB_SoftType
        {
            get{ return _cwb_softtype; }
            set{ _cwb_softtype = value; }
        }        
		/// <summary>
		/// 咨询类型（咨询、报错、操作、其它）
        /// </summary>		
		private int _cwb_calltype;
        public int CWB_CallType
        {
            get{ return _cwb_calltype; }
            set{ _cwb_calltype = value; }
        }        
		/// <summary>
		/// 服务类型（Ca平台、kms、wsus、其它）
        /// </summary>		
		private int _cwb_servicetype;
        public int CWB_ServiceType
        {
            get{ return _cwb_servicetype; }
            set{ _cwb_servicetype = value; }
        }        
		/// <summary>
		/// 问题描述
        /// </summary>		
		private string _cwb_description;
        public string CWB_Description
        {
            get{ return _cwb_description; }
            set{ _cwb_description = value; }
        }        
		/// <summary>
		/// 解决办法
        /// </summary>		
		private string _cwb_solution;
        public string CWB_Solution
        {
            get{ return _cwb_solution; }
            set{ _cwb_solution = value; }
        }        
		/// <summary>
		/// 创建人
        /// </summary>		
		private int _cwb_creater;
        public int CWB_Creater
        {
            get{ return _cwb_creater; }
            set{ _cwb_creater = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _cwb_createtime;
        public DateTime CWB_CreateTime
        {
            get{ return _cwb_createtime; }
            set{ _cwb_createtime = value; }
        }        
		/// <summary>
		/// 指定服务人
        /// </summary>		
		private int _cwb_foruser;
        public int CWB_ForUser
        {
            get{ return _cwb_foruser; }
            set{ _cwb_foruser = value; }
        }        
		/// <summary>
		/// 操作时间
        /// </summary>		
		private DateTime _cwb_operationtime;
        public DateTime CWB_OperationTime
        {
            get{ return _cwb_operationtime; }
            set{ _cwb_operationtime = value; }
        }        
		/// <summary>
		/// 工单状态（服务中、已完成、已回访）
        /// </summary>		
		private int _cwb_status;
        public int CWB_Status
        {
            get{ return _cwb_status; }
            set{ _cwb_status = value; }
        }        
		/// <summary>
		/// 备注
        /// </summary>		
		private string _cwb_remark;
        public string CWB_Remark
        {
            get{ return _cwb_remark; }
            set{ _cwb_remark = value; }
        }        
		   
	}
}