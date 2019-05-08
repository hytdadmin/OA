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
Date：2014-08-12 12:06:38
Description: BLL层 Call_SatisfactionNew
*/
namespace HYTD.BLL
{


	public class Call_SatisfactionNewBLL
	{

        Call_SatisfactionNewDAL service = new Call_SatisfactionNewDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddCall_SatisfactionNew(Call_SatisfactionNew entity)
        {
            service.AddCall_SatisfactionNew(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateCall_SatisfactionNew(Call_SatisfactionNew entity)
        {
            new Call_SatisfactionNewDAL().UpdateCall_SatisfactionNew(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Call_SatisfactionNew GetCall_SatisfactionNewEntity(int ID)
        {
            return service.GetCall_SatisfactionNewEntity(ID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCall_SatisfactionNew(int ID)
        {
            service.DeleteCall_SatisfactionNew(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_SatisfactionNew> GetCall_SatisfactionNewList()
        {
            return service.GetCall_SatisfactionNewList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Call_SatisfactionNew> GetCall_SatisfactionNewList(string strIP)
        {
            return service.GetCall_SatisfactionNewList(strIP);
        }
        /// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetCall_SatisfactionNewList(Call_SatisfactionNewTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetCall_SatisfactionNewList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }

		#endregion
   
	}
}