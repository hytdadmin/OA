using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class AdminTableTO
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		/// <summary>
		/// UserCode
        /// </summary>		
		private string _usercode;
        public string UserCode
        {
            get{ return _usercode; }
            set{ _usercode = value; }
        }        
		   
	}
}