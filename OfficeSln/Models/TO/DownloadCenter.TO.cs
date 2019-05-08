using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class DownloadCenterTO
	{
   		     
      	/// <summary>
		/// 下载中心表id
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
		/// 标题
        /// </summary>		
		private string _title;
        public string Title
        {
            get{ return _title; }
            set{ _title = value; }
        }        
		/// <summary>
		/// 附件重命名后名称
        /// </summary>		
		private string _affixnewname;
        public string AffixNewName
        {
            get{ return _affixnewname; }
            set{ _affixnewname = value; }
        }        
		/// <summary>
		/// 附件原名称
        /// </summary>		
		private string _affixoldname;
        public string AffixOldName
        {
            get{ return _affixoldname; }
            set{ _affixoldname = value; }
        }        
		/// <summary>
		/// 附件大小
        /// </summary>		
		private decimal _size;
        public decimal Size
        {
            get{ return _size; }
            set{ _size = value; }
        }        
		/// <summary>
		/// 附件类型/后缀
        /// </summary>		
		private string _suffix;
        public string Suffix
        {
            get{ return _suffix; }
            set{ _suffix = value; }
        }        
		/// <summary>
		/// 附件地址
        /// </summary>		
		private string _affixurl;
        public string AffixUrl
        {
            get{ return _affixurl; }
            set{ _affixurl = value; }
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
		/// 下载次数
        /// </summary>		
		private int _downnum;
        public int DownNum
        {
            get{ return _downnum; }
            set{ _downnum = value; }
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