using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace HYTD.BBS.Model.TO
{

    public class PostsTO
    {

        /// <summary>
        /// 主键
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 分类ID
        /// </summary>		
        private int _cid;
        public int CID
        {
            get { return _cid; }
            set { _cid = value; }
        }
        /// <summary>
        /// 问题主题
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 问题描述
        /// </summary>		
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 查看数
        /// </summary>		
        private int _lookcount;
        public int LookCount
        {
            get { return _lookcount; }
            set { _lookcount = value; }
        }
        /// <summary>
        /// 回复数
        /// </summary>		
        private int _recount;
        public int ReCount
        {
            get { return _recount; }
            set { _recount = value; }
        }
        /// <summary>
        /// 最后发表时间
        /// </summary>		
        private DateTime _lasttime;
        public DateTime LastTime
        {
            get { return _lasttime; }
            set { _lasttime = value; }
        }
        /// <summary>
        /// 最后发表人
        /// </summary>		
        private string _lastuser;
        public string LastUser
        {
            get { return _lastuser; }
            set { _lastuser = value; }
        }
        /// <summary>
        /// 发问人
        /// </summary>		
        private string _releaseuser;
        public string ReleaseUser
        {
            get { return _releaseuser; }
            set { _releaseuser = value; }
        }
        /// <summary>
        /// 发问时间
        /// </summary>		
        private DateTime _releasetime;
        public DateTime ReleaseTime
        {
            get { return _releasetime; }
            set { _releasetime = value; }
        }
        /// <summary>
        /// 是否反馈(0否 1是)
        /// </summary>		
        private int _isfeedback;
        public int isFeedback
        {
            get { return _isfeedback; }
            set { _isfeedback = value; }
        }
        /// <summary>
        /// 置顶(0否 1是)
        /// </summary>		
        private int _top;
        public int Top
        {
            get { return _top; }
            set { _top = value; }
        }
        /// <summary>
        /// 发帖人IP
        /// </summary>		
        private string _userip;
        public string UserIP
        {
            get { return _userip; }
            set { _userip = value; }
        }
        /// <summary>
        /// 是否匿名(0否 1是)
        /// </summary>		
        private int _isanonymity;
        public int isAnonymity
        {
            get { return _isanonymity; }
            set { _isanonymity = value; }
        }

        #region 自定义
        /// <summary>
        /// 热点问题数量
        /// </summary>		
        private string _hotcount;
        public string HotCount
        {
            get { return _hotcount; }
            set { _hotcount = value; }
        }
        /// <summary>
        /// 是否反馈   用于条件查询
        /// </summary>		
        private string _isback;
        public string isback
        {
            get { return _isback; }
            set { _isback = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime _Time;
        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        #endregion

    }
}