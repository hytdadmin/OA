using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace HYTD.BBS.Model.TO
{

    public class CategoryTO
    {
        /// <summary>
        /// 主键
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 状态（0停用 1可用 2 删除）
        /// </summary>		
        private int _satatus;
        public int satatus
        {
            get { return _satatus; }
            set { _satatus = value; }
        }

    }
}