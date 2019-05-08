using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class SayTO
	{
   		     
      	/// <summary>
		/// 说说表id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 发表人UserCode
        /// </summary>		
		private string _publishusercode;
        public string PublishUserCode
        {
            get{ return _publishusercode; }
            set{ _publishusercode = value; }
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
		/// 发表时间
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
		/// 是否删除(默认1未删除)；1未删除、0删除
        /// </summary>		
		private int _isdel;
        public int IsDel
        {
            get{ return _isdel; }
            set{ _isdel = value; }
        }
        /// <summary>
        /// 搜索时间
        /// </summary>		
        private string _searchtime;
        public string SearchTime
        {
            get { return _searchtime; }
            set { _searchtime = value; }
        }

        private int depId;
        /// <summary>
        /// 搜素部门
        /// </summary>
        public int DepId
        {
            get { return depId; }
            set { depId = value; }
        }   
		   
	}
}