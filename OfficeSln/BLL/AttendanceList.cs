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
Date：2013-09-17 10:59:00
Description: BLL层 AttendanceList
*/
namespace BLL
{


	public class AttendanceListBLL
	{
   		 
   		 AttendanceListDAL service = new AttendanceListDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddAttendanceList(AttendanceList entity)
        {
            service.AddAttendanceList(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAttendanceList(AttendanceList entity)
        {
            new AttendanceListDAL().UpdateAttendanceList(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AttendanceList GetAttendanceListEntity(int FID)
        {
            return service.GetAttendanceListEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAttendanceList(int FID)
        {
            service.DeleteAttendanceList(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<AttendanceList> GetAttendanceListList()
        {
            return service.GetAttendanceListList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetAttendanceListList(AttendanceListTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetAttendanceListList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}