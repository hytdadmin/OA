using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class FeedbackTO
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 创建人
        /// </summary>		
        private string _loginname;
        public string loginName
        {
            get { return _loginname; }
            set { _loginname = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _cratetime;
        public DateTime crateTime
        {
            get { return _cratetime; }
            set { _cratetime = value; }
        }
        /// <summary>
        /// 联系人方式
        /// </summary>		
        private string _contact;
        public string contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        /// <summary>
        /// 意见内容
        /// </summary>		
        private string _fbcontent;
        public string fbcontent
        {
            get { return _fbcontent; }
            set { _fbcontent = value; }
        }
        /// <summary>
        /// 意见类型（0 提建议 1有错误 2不会用 3其他）
        /// </summary>		
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 应用类型（0 提建议 1有错误 2不会用 3其他）
        /// </summary>		
        private int _apptype;
        public int apptype
        {
            get { return _apptype; }
            set { _apptype = value; }
        }
    }
}
