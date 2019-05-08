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
Date：2014-07-16 08:47:59
Description: BLL层 Call_Satisfaction_Item
*/
namespace HYTD.BLL
{


	public class Call_Satisfaction_ItemBLL
	{
   		 
   		 Call_Satisfaction_ItemDAL service = new Call_Satisfaction_ItemDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_Satisfaction_Item(Call_Satisfaction_Item entity)
        {
            service.AddCall_Satisfaction_Item(entity);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_Satisfaction_Item(Call_Satisfaction_Item entity)
        {
            new Call_Satisfaction_ItemDAL().UpdateCall_Satisfaction_Item(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Satisfaction_Item GetCall_Satisfaction_ItemEntity(int ID)
        {
            return service.GetCall_Satisfaction_ItemEntity(ID);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Satisfaction_Item GetCall_Satisfaction_ItemEntityBYCSID(int ID)
        {
            return service.GetCall_Satisfaction_ItemEntityBYCSID(ID);
        }


		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Satisfaction_Item(int ID)
        {
            service.DeleteCall_Satisfaction_Item(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_Satisfaction_Item> GetCall_Satisfaction_ItemList()
        {
            return service.GetCall_Satisfaction_ItemList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_Satisfaction_ItemList(Call_Satisfaction_ItemTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_Satisfaction_ItemList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}