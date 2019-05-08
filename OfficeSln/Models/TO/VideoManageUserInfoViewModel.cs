using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{
    public class VideoManageUserInfoViewModel
    {
        private string _UserCode;
        public string UserCode
        {
            get { return _UserCode; }
            set { _UserCode = value; }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _RoleName;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        private int _CategoryID;

        public int CategoryID
        {
            get { return _CategoryID; }
            set { _CategoryID = value; }
        }

        private string _CategoryName;

        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

    }
}
