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
Date：2014-07-16 08:47:56
Description: BLL层 Call_Satisfaction
*/
namespace HYTD.BLL
{


	public class Call_SatisfactionBLL
	{
   		 
   		 Call_SatisfactionDAL service = new Call_SatisfactionDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_Satisfaction(Call_Satisfaction entity)
        {
            service.AddCall_Satisfaction(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_Satisfaction(Call_Satisfaction entity)
        {
            new Call_SatisfactionDAL().UpdateCall_Satisfaction(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Satisfaction GetCall_SatisfactionEntity(int ID)
        {
            return service.GetCall_SatisfactionEntity(ID);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_Satisfaction GetCall_SatisfactionEntityBYCWBID(int ID)
        {
            return service.GetCall_SatisfactionEntityBYCWBID(ID);
        }


		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_Satisfaction(int ID)
        {
            service.DeleteCall_Satisfaction(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_Satisfaction> GetCall_SatisfactionList()
        {
            return service.GetCall_SatisfactionList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_SatisfactionList(Call_SatisfactionTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_SatisfactionList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
	}
}