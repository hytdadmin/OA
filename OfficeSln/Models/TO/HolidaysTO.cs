using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.TO
{

    public class HolidaysTO
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>		
        private string _usercode;
        public string UserCode
        {
            get { return _usercode; }
            set { _usercode = value; }
        }
        /// <summary>
        /// 用户名字
        /// </summary>
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        /// <summary>
        /// 时间间隔
        /// </summary>		
        private string _datetime;
        public string DateTime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }
        /// <summary>
        /// 天数
        /// </summary>		
        private decimal _daycount;
        public decimal DayCount
        {
            get { return _daycount; }
            set { _daycount = value; }
        }
        /// <summary>
        /// 假期类型（1.年假  2.倒休 3.加班）
        /// </summary>		
        private int _holidaystype;
        public int HolidaysType
        {
            get { return _holidaystype; }
            set { _holidaystype = value; }
        }
        /// <summary>
        /// 说明
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        private string _stratTime;

        public string StratTime
        {
            get { return _stratTime; }
            set { _stratTime = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        private string _endTime;

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
    } 
}
