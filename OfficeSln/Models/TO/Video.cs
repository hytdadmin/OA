using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class Video
    {
        private string _Name;
        public string Name 
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Url;

        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _CreateTime;
        public string CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
    }
}
