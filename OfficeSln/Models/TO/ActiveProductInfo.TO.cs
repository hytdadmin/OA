using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
   public class ActiveProductInfoTO
    {
        #region Model
        private int _id;
        private int _customerid;
        private string _productid;
        private string _errormessage;
        private string _loginname;
        private DateTime? _createdate;
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
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage
        {
            set { _errormessage = value; }
            get { return _errormessage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model
    }
}
