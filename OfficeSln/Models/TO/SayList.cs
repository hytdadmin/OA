using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    /// <summary>
    /// 说说列表实体类
    /// </summary>
    public class SayList
    {
        private int _Id=0;
        private string _Contents=string.Empty;
        private string _UserName=string.Empty;
        private DateTime? _PublishTime = DateTime.MinValue;
        private int _UserId = 0;
        private string userCode=string.Empty;


        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime? PublishTime
        {
            get { return _PublishTime; }
            set { _PublishTime = value; }
        }

        /// <summary>
        /// 说说发表的用户
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>
        /// 说说内容
        /// </summary>
        public string Contents
        {
            get { return _Contents; }
            set { _Contents = value; }
        }

        /// <summary>
        /// 说说id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        /// <summary>
        /// 用户code
        /// </summary>
        public string UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }
    }
}
