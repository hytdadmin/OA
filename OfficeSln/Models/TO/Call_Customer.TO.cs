using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.Model.TO
{

	public class Call_CustomerTO
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>		
		private int _cc_id;
        public int CC_ID
        {
            get{ return _cc_id; }
            set{ _cc_id = value; }
        }        
		/// <summary>
		/// 客户名称
        /// </summary>		
		private string _cc_name;
        public string CC_Name
        {
            get{ return _cc_name; }
            set{ _cc_name = value; }
        }        
		/// <summary>
		/// 联系人
        /// </summary>		
		private string _cc_username;
        public string CC_UserName
        {
            get{ return _cc_username; }
            set{ _cc_username = value; }
        }        
		/// <summary>
		/// 联系电话
        /// </summary>		
		private string _cc_tel;
        public string CC_Tel
        {
            get{ return _cc_tel; }
            set{ _cc_tel = value; }
        }        
		/// <summary>
		/// 客户类型
        /// </summary>		
		private int _cc_type;
        public int CC_Type
        {
            get{ return _cc_type; }
            set{ _cc_type = value; }
        }        
		/// <summary>
		/// 创建时间
        /// </summary>		
		private DateTime _cc_createtime;
        public DateTime CC_CreateTime
        {
            get{ return _cc_createtime; }
            set{ _cc_createtime = value; }
        }        
		/// <summary>
		/// 服务开始时间
        /// </summary>		
		private DateTime _cc_servicestarttime;
        public DateTime CC_ServiceStartTime
        {
            get{ return _cc_servicestarttime; }
            set{ _cc_servicestarttime = value; }
        }        
		/// <summary>
		/// 服务结束时间
        /// </summary>		
		private DateTime _cc_serviceendtime;
        public DateTime CC_ServiceEndTime
        {
            get{ return _cc_serviceendtime; }
            set{ _cc_serviceendtime = value; }
        }        
		/// <summary>
		/// 信息表url
        /// </summary>		
		private string _cc_url;
        public string CC_Url
        {
            get{ return _cc_url; }
            set{ _cc_url = value; }
        }        
		/// <summary>
		/// 备注
        /// </summary>		
		private string _cc_remark;
        public string CC_Remark
        {
            get{ return _cc_remark; }
            set{ _cc_remark = value; }
        }        
		/// <summary>
		/// 状态
        /// </summary>		
		private int _cc_status;
        public int CC_Status
        {
            get{ return _cc_status; }
            set{ _cc_status = value; }
        }        
		/// <summary>
		/// 合同时间
        /// </summary>		
		private DateTime _cc_httime;
        public DateTime CC_HTTime
        {
            get{ return _cc_httime; }
            set{ _cc_httime = value; }
        }
        /// <summary>
        /// 负责工程师
        /// </summary>		
        private string _cc_owner;
        public string CC_Owner
        {
            get { return _cc_owner; }
            set { _cc_owner = value; }
        }   
	}
}