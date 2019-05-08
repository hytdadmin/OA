using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace HYTD.Models.TO
{
    public class Call_FAQListTO
    {
        /// <summary>
        /// 主键
        /// </summary>		
        private int _cf_id;
        public int CF_ID
        {
            get { return _cf_id; }
            set { _cf_id = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>		
        private string _cf_describe;
        public string CF_Describe
        {
            get { return _cf_describe; }
            set { _cf_describe = value; }
        }
        /// <summary>
        /// 问题列表
        /// </summary>		
        private string _cf_errorlist;
        public string CF_ErrorList
        {
            get { return _cf_errorlist; }
            set { _cf_errorlist = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>		
        private DateTime _cf_adddate;
        public DateTime CF_AddDate
        {
            get { return _cf_adddate; }
            set { _cf_adddate = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>		
        private DateTime _cf_enddate;
        public DateTime CF_EndDate
        {
            get { return _cf_enddate; }
            set { _cf_enddate = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>		
        private string _cf_content;
        public string CF_Content
        {
            get { return _cf_content; }
            set { _cf_content = value; }
        }

        /// <summary>
        /// 软件类型
        /// </summary>		
        private int _cf_softtypeid;
        public int CF_SoftTypeID
        {
            get { return _cf_softtypeid; }
            set { _cf_softtypeid = value; }
        }
        /// <summary>
        /// 创建者
        /// </summary>		
        private int _cf_userid;
        public int CF_UserID
        {
            get { return _cf_userid; }
            set { _cf_userid = value; }
        }

    }
}
