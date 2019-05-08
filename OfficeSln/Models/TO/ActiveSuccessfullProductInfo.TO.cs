using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Models.TO
{

	public class ActiveSuccessfullProductInfoTO
    {  
        /// <summary>
        /// 激活时间
        /// </summary>
        public string StartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 激活结束时间
        /// </summary>
        public string EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
   		     
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
		/// CustomerID
        /// </summary>		
		private int _customerid;
        public int CustomerID
        {
            get{ return _customerid; }
            set{ _customerid = value; }
        }        
		/// <summary>
		/// ProductID
        /// </summary>		
		private string _productid;
        public string ProductID
        {
            get{ return _productid; }
            set{ _productid = value; }
        }        
		/// <summary>
		/// ErrorMessage
        /// </summary>		
		private string _errormessage;
        public string ErrorMessage
        {
            get{ return _errormessage; }
            set{ _errormessage = value; }
        }        
		/// <summary>
		/// LoginName
        /// </summary>		
		private string _loginname;
        public string LoginName
        {
            get{ return _loginname; }
            set{ _loginname = value; }
        }        
		/// <summary>
		/// CreateDate
        /// </summary>		
		private DateTime _createdate;
        public DateTime CreateDate
        {
            get{ return _createdate; }
            set{ _createdate = value; }
        }        
		   
	}
}