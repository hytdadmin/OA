using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class BulletinTO
	{
   		     
      	/// <summary>
		/// 公告表id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 发布人usercode
        /// </summary>		
		private string _publishusercode;
        public string PublishUserCode
        {
            get{ return _publishusercode; }
            set{ _publishusercode = value; }
        }        
		/// <summary>
		/// 标题
        /// </summary>		
		private string _title;
        public string Title
        {
            get{ return _title; }
            set{ _title = value; }
        }        
		/// <summary>
		/// 内容
        /// </summary>		
		private string _contents;
        public string Contents
        {
            get{ return _contents; }
            set{ _contents = value; }
        }        
		/// <summary>
		/// 发布时间
        /// </summary>		
		private DateTime _publishtime;
        public DateTime PublishTime
        {
            get{ return _publishtime; }
            set{ _publishtime = value; }
        }        
		/// <summary>
		/// 浏览次数
        /// </summary>		
		private int _scannum;
        public int ScanNum
        {
            get{ return _scannum; }
            set{ _scannum = value; }
        }        
		/// <summary>
		/// 排序字段
        /// </summary>		
		private int _orderid;
        public int OrderId
        {
            get{ return _orderid; }
            set{ _orderid = value; }
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

        private string pubName;

        /// <summary>
        /// 发布人名称
        /// </summary>
        public string PubName
        {
            get { return pubName; }
            set { pubName = value; }
        }

        private string pubDep;

        /// <summary>
        /// 发布部门
        /// </summary>
        public string PubDep
        {
            get { return pubDep; }
            set { pubDep = value; }
        }

        private int pubDepId;

        /// <summary>
        /// 发布部门id
        /// </summary>
        public int PubDepId
        {
            get { return pubDepId; }
            set { pubDepId = value; }
        }

        private DateTime? startDt;

        /// <summary>
        /// 搜索用开始时间
        /// </summary>
        public DateTime? StartDt
        {
            get { return startDt; }
            set { startDt = value; }
        }

        private DateTime? endDt;
        /// <summary>
        /// 搜索用结束时间
        /// </summary>
        public DateTime? EndDt
        {
            get { return endDt; }
            set { endDt = value; }
        }
	}
}