using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class VideoCategory
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _VideoCategoryName;
        public string VideoCategoryName
        {
            get { return _VideoCategoryName; }
            set { _VideoCategoryName = value; }
        }

        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        private int _RowCount;

        public int RowCount
        {
            get { return _RowCount; }
            set { _RowCount = value; }
        }


        private List<Video> _Videos = new List<Video>();
        public List<Video> Videos
        {
            get { return _Videos; }
            set { _Videos = value; }
        }

    }
}
