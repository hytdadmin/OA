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
Date：2014-01-10 16:28:31
Description: BLL层 Call_WorkBillHistory
*/
namespace HYTD.BLL
{


	public class Call_WorkBillHistoryBLL
	{
   		 
   		 Call_WorkBillHistoryDAL service = new Call_WorkBillHistoryDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_WorkBillHistory(Call_WorkBillHistory entity)
        {
            service.AddCall_WorkBillHistory(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_WorkBillHistory(Call_WorkBillHistory entity)
        {
            new Call_WorkBillHistoryDAL().UpdateCall_WorkBillHistory(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_WorkBillHistory GetCall_WorkBillHistoryEntity(int FID)
        {
            return service.GetCall_WorkBillHistoryEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_WorkBillHistory(int FID)
        {
            service.DeleteCall_WorkBillHistory(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_WorkBillHistory> GetCall_WorkBillHistoryList()
        {
            return service.GetCall_WorkBillHistoryList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_WorkBillHistoryList(Call_WorkBillHistoryTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_WorkBillHistoryList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}