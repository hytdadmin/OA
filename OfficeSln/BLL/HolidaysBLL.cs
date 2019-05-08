using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Models.TO;

namespace BLL
{
    public class HolidaysBLL
    {

        HolidaysDAL service = new HolidaysDAL();



        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddHolidaysTable(HolidaysTable entity)
        {
            service.AddHolidaysTable(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateHolidaysTable(HolidaysTable entity)
        {
            new HolidaysDAL().UpdateHolidaysTable(entity);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteHolidaysTable(int ID)
        {
          return  service.DeleteHolidaysTable(ID);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public HolidaysTable GetHolidaysTableEntity(int guid)
        {
            return service.GetHolidaysTableEntity(guid);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<HolidaysTable> GetHolidaysTableList()
        {
            return service.GetHolidaysTableList();
        }


        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetHolidaysTableList(HolidaysTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetHolidaysTableList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }

        /// <summary>
        /// 获取用户假期
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public DataTable GetUserHolidays(string userCode)
        {
            return service.GetUserHolidays(userCode);
        }
        #endregion

          /// <summary>
        /// 假期统计信息
        /// </summary>
        public DataTable GetHolidaysStatisInfor(HolidaysTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetHolidaysStatisInfor(TO,pageIndex,pageSize,orderBy,out rowCount);
        }
     
    }
}
