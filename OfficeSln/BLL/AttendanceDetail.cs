using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Models;
using System.Data;
using Model.TO;
/*
Author：liulei
Version：1.0
Date：2013-09-17 10:58:58
Description: BLL层 AttendanceDetail
*/
namespace BLL
{


	public class AttendanceDetailBLL
	{
   		 
   		 AttendanceDetailDAL service = new AttendanceDetailDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddAttendanceDetail(AttendanceDetail entity)
        {
            service.AddAttendanceDetail(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAttendanceDetail(AttendanceDetail entity)
        {
            new AttendanceDetailDAL().UpdateAttendanceDetail(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AttendanceDetail GetAttendanceDetailEntity(int FID)
        {
            return service.GetAttendanceDetailEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAttendanceDetail(int FID)
        {
            service.DeleteAttendanceDetail(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<AttendanceDetail> GetAttendanceDetailList()
        {
            return service.GetAttendanceDetailList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetAttendanceDetailList(AttendanceDetailTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetAttendanceDetailList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}