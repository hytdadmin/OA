using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class PositionTO
	{
   		     
      	/// <summary>
		/// 职位表id
        /// </summary>		
		private int _id;
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// 职位名称
        /// </summary>		
		private string _name;
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
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