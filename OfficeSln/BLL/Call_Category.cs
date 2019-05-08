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
Date：2014-01-13 09:53:08
Description: BLL层 Call_Category
*/
namespace HYTD.BLL
{


	public class Call_CategoryBLL
	{
   		 
   		 Call_CategoryDAL service = new Call_CategoryDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_Category(Call_Category entity)
        {
            service.AddCall_Category(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_Category(Call_Category entity)
        {
            new Call_CategoryDAL().UpdateCall_Category(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Category GetCall_CategoryEntity(int FID)
        {
            return service.GetCall_CategoryEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Category(int FID)
        {
            service.DeleteCall_Category(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_Category> GetCall_CategoryList()
        {
            return service.GetCall_CategoryList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_CategoryList(Call_CategoryTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_CategoryList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}