using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace Model.TO
{

	public class SensitiveWordsTO
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
		/// sensitiveWord
        /// </summary>		
		private string _sensitiveword;
        public string sensitiveWord
        {
            get{ return _sensitiveword; }
            set{ _sensitiveword = value; }
        }        
		/// <summary>
		/// status
        /// </summary>		
		private int _status;
        public int status
        {
            get{ return _status; }
            set{ _status = value; }
        }        
		   
	}
}