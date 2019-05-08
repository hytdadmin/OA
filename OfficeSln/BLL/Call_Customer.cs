using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYTD.DAL;
using HYTD.Model;
using System.Data;
using HYTD.Model.TO;
using Models;
/*
Author：liulei
Version：1.0
Date：2014-01-10 16:22:14
Description: BLL层 Call_Customer
*/
namespace HYTD.BLL
{


	public class Call_CustomerBLL
	{
   		 
   		 Call_CustomerDAL service = new Call_CustomerDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_Customer(Call_Customer entity)
        {
            service.AddCall_Customer(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_Customer(Call_Customer entity)
        {
            new Call_CustomerDAL().UpdateCall_Customer(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Customer GetCall_CustomerEntity(int FID)
        {
            return service.GetCall_CustomerEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Customer(int FID)
        {
            service.DeleteCall_Customer(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_Customer> GetCall_CustomerList()
        {
            return service.GetCall_CustomerList();
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_Customer> GetCall_CustomerList(string strName)
        {
            return service.GetCall_CustomerList(strName);
        }

		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_CustomerList(Call_CustomerTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_CustomerList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}