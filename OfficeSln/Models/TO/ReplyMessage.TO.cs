using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class ReplyMessageTO
	{
   		     
      	/// <summary>
		/// 评论表id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 发布人UserCode
        /// </summary>		
		private string _publishusercode;
        public string PublishUserCode
        {
            get{ return _publishusercode; }
            set{ _publishusercode = value; }
        }        
		/// <summary>
		/// 标示评论说说/日志，如果是说说则为1，日志则为2
        /// </summary>		
		private int _typeid;
        public int TypeId
        {
            get{ return _typeid; }
            set{ _typeid = value; }
        }        
		/// <summary>
		/// 评论的信息id/评论的评论id(取决于TypeId)
        /// </summary>		
		private int _messageid;
        public int MessageId
        {
            get{ return _messageid; }
            set{ _messageid = value; }
        }        
		/// <summary>
		/// 评论内容
        /// </summary>		
		private string _contents;
        public string Contents
        {
            get{ return _contents; }
            set{ _contents = value; }
        }        
		/// <summary>
		/// 评论时间
        /// </summary>		
		private DateTime _publishtime;
        public DateTime PublishTime
        {
            get{ return _publishtime; }
            set{ _publishtime = value; }
        }        
		/// <summary>
		/// 是否删除(默认1未删除)；1未删除、0删除
        /// </summary>		
		private int _isdel;
        public int IsDel
        {
            get{ return _isdel; }
            set{ _isdel = value; }
        }        
		   
	}
}