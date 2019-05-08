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
Date：2013-09-17 10:59:02
Description: BLL层 Department
*/
namespace BLL
{


	public class DepartmentBLL
	{
   		 
   		 DepartmentDAL service = new DepartmentDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddDepartment(Department entity)
        {
            service.AddDepartment(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateDepartment(Department entity)
        {
            new DepartmentDAL().UpdateDepartment(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Department GetDepartmentEntity(int FID)
        {
            return service.GetDepartmentEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDepartment(int FID)
        {
            service.DeleteDepartment(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartmentList()
        {
            return service.GetDepartmentList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartmentList(DepartmentTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetDepartmentList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
        /// <summary>
        /// 返回所有部门
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepListByIsdel()
        {
            return service.GetDepListByIsdel();
        }
	}
}