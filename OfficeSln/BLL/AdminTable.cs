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
Date：2013-09-17 10:58:57
Description: BLL层 AdminTable
*/
namespace BLL
{


	public class AdminTableBLL
	{
   		 
   		 AdminTableDAL service = new AdminTableDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddAdminTable(AdminTable entity)
        {
            service.AddAdminTable(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateAdminTable(AdminTable entity)
        {
            new AdminTableDAL().UpdateAdminTable(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public AdminTable GetAdminTableEntity(int FID)
        {
            return service.GetAdminTableEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdminTable(int FID)
        {
            service.DeleteAdminTable(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<AdminTable> GetAdminTableList()
        {
            return service.GetAdminTableList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetAdminTableList(AdminTableTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetAdminTableList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}