using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class DepartmentUserInfoViewModel
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _RoleName;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }



        private List<VideoManageUserInfoViewModel> _Users = new List<VideoManageUserInfoViewModel>();
        public List<VideoManageUserInfoViewModel> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }
    }
}
