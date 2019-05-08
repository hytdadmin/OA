using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace HYTD.BBS.Model.TO
{


    public class RePostsTO
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
        /// 父ID
        /// </summary>		
        private int _fatherid;
        public int FatherID
        {
            get { return _fatherid; }
            set { _fatherid = value; }
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
        /// 父回帖ID
        /// </summary>		
        private int _releasefatherid;
        public int ReleaseFatherID
        {
            get { return _releasefatherid; }
            set { _releasefatherid = value; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>		
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>		
        private DateTime _redatetime;
        public DateTime ReDatetime
        {
            get { return _redatetime; }
            set { _redatetime = value; }
        }
        /// <summary>
        /// 回复人
        /// </summary>		
        private string _reuser;
        public string ReUser
        {
            get { return _reuser; }
            set { _reuser = value; }
        }
        /// <summary>
        /// 是否反馈贴(0否 1是)
        /// </summary>		
        private int _isrefeedback;
        public int isReFeedback
        {
            get { return _isrefeedback; }
            set { _isrefeedback = value; }
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
        /// IP
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

        #region 自定义属性
        /// <summary>
        /// 主题
        /// </summary>
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        /// <summary>
        /// 时间
        /// </summary>
        private string endtime;

        public string EndTime
        {
            get { return endtime; }
            set { endtime = value; }
        }
       
        /// <summary>
        /// f发布人
        /// </summary>
        private string sendUser;

        public string SendUser
        {
            get { return sendUser; }
            set { sendUser = value; }
        }
        #endregion

    }
}