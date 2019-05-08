using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class VideosTo
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _VideoName;
        public string VideoName
        {
            get { return _VideoName; }
            set { _VideoName = value; }
        }

        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private int _VideoCategory;
        public int VideoCategory
        {
            get { return _VideoCategory; }
            set { _VideoCategory = value; }
        }

        private string _VideoCategoryName;
        public string VideoCategoryName
        {
            get { return _VideoCategoryName; }
            set { _VideoCategoryName = value; }
        }

        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }



    }
}
