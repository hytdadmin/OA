using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class VideoManageUserInfoAssignViewModel
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

        private List<VideoManageUserInfoViewModel> _Users = new List<VideoManageUserInfoViewModel>();
        public List<VideoManageUserInfoViewModel> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }
    }
}
