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
Date：2013-10-12 15:25:03
Description: BLL层 VisitTb
*/
namespace BLL
{


	public class VisitTbBLL
	{
   		 
   		 VisitTbDAL service = new VisitTbDAL();
   		 

		
		#region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void AddVisitTb(VisitTb entity)
        {
            service.AddVisitTb(entity);
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateVisitTb(VisitTb entity)
        {
            new VisitTbDAL().UpdateVisitTb(entity);
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public VisitTb GetVisitTbEntity(int FID)
        {
            return service.GetVisitTbEntity(FID);
        }



		/// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteVisitTb(int FID)
        {
            service.DeleteVisitTb(FID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<VisitTb> GetVisitTbList()
        {
            return service.GetVisitTbList();
        }


		/// <summary>
        /// 获得数据列表分页
        /// </summary>
        /// <returns></returns>
        public DataTable GetVisitTbList(VisitTbTO TO, int pageIndex, int pageSize, string orderBy, out int rowCount)
        {
            return service.GetVisitTbList(TO, pageIndex, pageSize, orderBy, out rowCount);
        }


		#endregion
   
        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public int GetVisitCount(int min)
        {
            return service.GetVisitCount(min);
        }
	}
}